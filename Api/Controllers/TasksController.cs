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
            IEnumerable<Entities.Task> result = _context.Tasks;
            if (!string.IsNullOrWhiteSpace(query))
            {
                // Check both title and tags for the query string, case insensitive
                result = result.Where(t => t.Title.Contains(query, StringComparison.OrdinalIgnoreCase)
                || t.Tags.Any(s => s.Contains(query, StringComparison.OrdinalIgnoreCase)));
            }

            if (completed.HasValue)
            {
                result = result.Where(t => t.Completed == completed.Value);
            }

            return Ok(result.ToList());
        }

        [HttpPost]
        public ActionResult<Entities.Task> Post([FromBody] Entities.Task newTask)
        {
            if (!IsValidTitle(newTask.Title))
            {
                return BadRequest($"Invalid title");
            }

            newTask.Id = 0; // Ensure the ID is zero so that EF Core will generate a new one
            newTask.CreatedAt = DateTime.UtcNow;
            newTask.UpdatedAt = DateTime.UtcNow;
            _context.Tasks.Add(newTask);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = newTask.Id }, newTask);
        }

        [HttpPut("{id}")]
        public ActionResult<Entities.Task> Put(int id, [FromBody] JsonElement updates)
        {
            Entities.Task? existingTask = _context.Tasks.Find(id);
            if (existingTask is null)
            {
                return NotFound();
            }

            // Complete validation before applying any updates
            if (updates.TryGetProperty("title", out JsonElement titleElement))
            {
                string newTitle = titleElement.GetString() ?? string.Empty;
                if (!IsValidTitle(newTitle))
                {
                    return BadRequest($"Invalid title");
                }
            }
            if (updates.TryGetProperty("completed", out JsonElement completedElement))
            {
                if (!completedElement.ValueKind.Equals(JsonValueKind.True) && !completedElement.ValueKind.Equals(JsonValueKind.False))
                {
                    return BadRequest("Completed must be a boolean");
                }
            }
            if (updates.TryGetProperty("tags", out JsonElement tagsElement))
            {
                if (tagsElement.ValueKind != JsonValueKind.Array)
                {
                    return BadRequest("Tags must be an array of strings");
                }
                foreach (JsonElement tag in tagsElement.EnumerateArray())
                {
                    if (tag.ValueKind != JsonValueKind.String)
                    {
                        return BadRequest("Each tag must be a string");
                    }
                }
            }

            // Apply updates
            if (updates.TryGetProperty("title", out titleElement))
            {
                existingTask.Title = titleElement.GetString() ?? existingTask.Title;
            }
            if (updates.TryGetProperty("completed", out completedElement))
            {
                existingTask.Completed = completedElement.GetBoolean();
            }
            if (updates.TryGetProperty("dueDate", out JsonElement dueDateElement))
            {
                if (dueDateElement.ValueKind == JsonValueKind.Null)
                {
                    existingTask.DueDate = null;
                }
                else if (dueDateElement.ValueKind == JsonValueKind.String && DateTime.TryParse(dueDateElement.GetString(), out DateTime dueDate))
                {
                    existingTask.DueDate = dueDate;
                }
                else
                {
                    return BadRequest("DueDate must be a valid date or null");
                }
            }
            if (updates.TryGetProperty("tags", out tagsElement))
            {
                existingTask.Tags = [.. tagsElement.EnumerateArray().Select(t => t.GetString() ?? string.Empty)];
            }

            // Probably fine to update the UpdatedAt even if no fields changed
            existingTask.UpdatedAt = DateTime.UtcNow;
            _context.SaveChanges();
            return Ok(existingTask);
        }

        private static bool IsValidTitle(string title) =>
            title.Length >= Entities.Task.TitleMinLength &&
            title.Length <= Entities.Task.TitleMaxLength;

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Entities.Task? task = _context.Tasks.Find(id);
            if (task is null)
            {
                return NotFound();
            }
            _context.Tasks.Remove(task);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
