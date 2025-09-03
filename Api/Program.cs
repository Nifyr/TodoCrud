using Microsoft.EntityFrameworkCore;
using TodoCrud.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Use InMemoryDatabase for testing. SQLite is default for simplicity.
var useSqlite = builder.Configuration.GetValue("UseSqlite", true);
if (useSqlite)
{
    var dbPath = Path.Combine(AppContext.BaseDirectory, "todo.db");
    builder.Services.AddDbContext<TodoContext>(options => options.UseSqlite($"Data Source={dbPath}"));
    Console.WriteLine($"Using SQLite database at {dbPath}");
}
else
{
    builder.Services.AddDbContext<TodoContext>(options => options.UseInMemoryDatabase("TodoDb"));
    Console.WriteLine("Using In-Memory database");
}

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TodoContext>();
    db.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }