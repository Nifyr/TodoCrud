using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TodoCrud.Api.Data;
using TodoCrud.Entities;

namespace TodoCrud.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TasksController(TodoContext context) : ControllerBase
    {
        private readonly TodoContext _context = context;

        [HttpGet]
        public ActionResult<IEnumerable<Entities.Task>> Get([FromQuery] string? query, [FromQuery] bool? completed)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public ActionResult<Entities.Task> Post([FromBody] Entities.Task newTask)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{id}")]
        public ActionResult<Entities.Task> Put(int id, [FromBody] JsonElement updates)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
