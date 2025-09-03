using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;

namespace TodoCrud.Api.Tests
{
    [TestClass]
    public sealed class TasksControllerTests
    {

        private static WebApplicationFactory<Program> _factory = null!;
        private HttpClient _client = null!;

        [TestInitialize]
        public void Setup()
        {
            _factory ??= new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.UseEnvironment("Testing");
                });

            _client = _factory.CreateClient();
        }

        [TestMethod]
        public async Task EndpointsExist()
        {
            // Check GET
            var getResponse = await _client.GetAsync("/tasks");
            Assert.AreNotEqual(HttpStatusCode.NotFound, getResponse.StatusCode);

            // Check POST
            var postResponse = await _client.PostAsJsonAsync("/tasks", new Entities.Task { Title = "Test", Completed = false });
            Assert.AreNotEqual(HttpStatusCode.NotFound, postResponse.StatusCode);

            var created = await postResponse.Content.ReadFromJsonAsync<Entities.Task>();
            Assert.IsNotNull(created);

            // Check PUT
            var putResponse = await _client.PutAsJsonAsync($"/tasks/{created!.Id}", new { Title = "Updated" });
            Assert.AreNotEqual(HttpStatusCode.NotFound, putResponse.StatusCode);

            // Check DELETE
            var deleteResponse = await _client.DeleteAsync($"/tasks/{created.Id}");
            Assert.AreNotEqual(HttpStatusCode.NotFound, deleteResponse.StatusCode);
        }

        [TestMethod]
        public async Task InputValidation_Works()
        {
            // Title too short
            var response1 = await _client.PostAsJsonAsync("/tasks", new Entities.Task { Title = "", Completed = false });
            Assert.AreEqual(HttpStatusCode.BadRequest, response1.StatusCode);

            // Title too long
            var longTitle = new string('a', Entities.Task.TitleMaxLength + 1);
            var response2 = await _client.PostAsJsonAsync("/tasks", new Entities.Task { Title = longTitle, Completed = false });
            Assert.AreEqual(HttpStatusCode.BadRequest, response2.StatusCode);

            // DELETE non-existing
            var response3 = await _client.DeleteAsync("/tasks/9999");
            Assert.AreEqual(HttpStatusCode.NotFound, response3.StatusCode);
        }

        [TestMethod]
        public async Task Get_WithQueryAndCompleted_FiltersCorrectly()
        {
            // Seed tasks
            await _client.PostAsJsonAsync("/tasks", new Entities.Task { Title = "Work Task", Completed = false });
            await _client.PostAsJsonAsync("/tasks", new Entities.Task { Title = "Done Task", Completed = true });

            // Filter by completed=true
            var response = await _client.GetFromJsonAsync<Entities.Task[]>("/tasks?completed=true");
            Assert.IsNotNull(response);
            Assert.IsTrue(response!.All(t => t.Completed));

            // Filter by query
            var queryResponse = await _client.GetFromJsonAsync<Entities.Task[]>("/tasks?query=Work");
            Assert.IsNotNull(queryResponse);
            Assert.IsTrue(queryResponse!.All(t => t.Title.Contains("Work")));
        }

        [TestMethod]
        public async Task Put_PartialUpdate_Works()
        {
            // Create task
            var created = await _client.PostAsJsonAsync("/tasks", new Entities.Task { Title = "Original", Completed = false });
            var task = await created.Content.ReadFromJsonAsync<Entities.Task>();
            Assert.IsNotNull(task);

            // Partial update: only Title
            var putResponse = await _client.PutAsJsonAsync($"/tasks/{task!.Id}", new { Title = "Updated" });
            var updatedTask = await putResponse.Content.ReadFromJsonAsync<Entities.Task>();

            Assert.AreEqual("Updated", updatedTask!.Title);
            Assert.AreEqual(false, updatedTask.Completed); // unchanged
        }
    }
}
