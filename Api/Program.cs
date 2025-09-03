using Microsoft.EntityFrameworkCore;
using System.Text.Json;
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

    // Seed initial data if the database is empty
    if (!db.Tasks.Any())
    {
        var seedFile = Path.Combine(AppContext.BaseDirectory, "seed.json");
        if (File.Exists(seedFile))
        {
            var json = File.ReadAllText(seedFile);
            var tasks = JsonSerializer.Deserialize<List<TodoCrud.Entities.Task>>(json, jsonOptions) ?? [];

            db.Tasks.AddRange(tasks);
            db.SaveChanges();
            Console.WriteLine($"Seeded {tasks.Count} tasks.");
        }
    }
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

public partial class Program 
{
    public static readonly JsonSerializerOptions jsonOptions = new(JsonSerializerDefaults.Web);
}