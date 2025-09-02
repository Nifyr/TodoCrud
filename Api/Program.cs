using Microsoft.EntityFrameworkCore;
using TodoCrud.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Using In-Memory Database for simplicity. Can be replaced with a real database provider.
builder.Services.AddDbContext<TodoContext>(options => options.UseInMemoryDatabase("TodoDb"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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