using Microsoft.AspNetCore.Mvc;
using TodoCrud.Entities;

namespace TodoCrud.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TasksController(ILogger<TasksController> logger) : ControllerBase
    {
        private readonly ILogger<TasksController> _logger = logger;

        [HttpGet(Name = "GetTasks")]
        public IEnumerable<Entities.Task> Get()
        {
            return
            [
                new Entities.Task
                {
                    Id = 1,
                    Title = "Sample Task",
                    Completed = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Entities.Task
                {
                    Id = 2,
                    Title = "Another Task",
                    Completed = true,
                    CreatedAt = DateTime.UtcNow.AddDays(-1),
                    DueDate = DateTime.UtcNow.AddDays(7),
                    Tags = ["work", "urgent"],
                    UpdatedAt = DateTime.UtcNow.AddDays(-1)
                },
                new Entities.Task
                {
                    Id = 3,
                    Title = "Third Task",
                    Completed = false,
                    CreatedAt = DateTime.UtcNow.AddDays(-2),
                    Tags = ["personal"],
                    UpdatedAt = DateTime.UtcNow
                }
            ];
        }
    }
}
