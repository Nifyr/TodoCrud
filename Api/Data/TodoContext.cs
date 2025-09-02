using Microsoft.EntityFrameworkCore;

namespace TodoCrud.Api.Data
{
    public class TodoContext(DbContextOptions<TodoContext> options) : DbContext(options)
    {
        public DbSet<Entities.Task> Tasks { get; set; } = null!;
    }
}
