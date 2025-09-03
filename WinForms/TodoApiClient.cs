using System.Net.Http.Json;

namespace TodoCrud.WinForms
{
    internal class TodoApiClient()
    {
        private const string BASE_URL = "http://localhost:5185/";

        private readonly HttpClient _client = new()
        {
            BaseAddress = new Uri(BASE_URL)
        };

        internal struct SearchFilter
        {
            public string SearchString { get; set; }
            public bool IncludeCompleted { get; set; }
        }

        internal struct ApiResponse<T>
        {
            public bool Success { get; set; }
            public T? Content { get; set; }
            public string? ErrorMessage { get; set; }
        }

        // Centralized method to handle API requests with error handling and deserialization
        private static ApiResponse<T> ExecuteWithHandling<T>(Func<HttpResponseMessage> requestFunc)
        {
            try
            {
                HttpResponseMessage response = requestFunc();
                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResponse<T>
                    {
                        Success = false,
                        ErrorMessage = $"API returned status code {response.StatusCode}"
                    };
                }
                T? content = response.Content.ReadFromJsonAsync<T>().Result;
                if (content is null)
                {
                    return new ApiResponse<T>
                    {
                        Success = false,
                        ErrorMessage = "Failed to deserialize API response"
                    };
                }
                return new ApiResponse<T>
                {
                    Success = true,
                    Content = content
                };
            }
            catch (HttpRequestException ex)
            {
                return new ApiResponse<T>
                {
                    Success = false,
                    ErrorMessage = $"HTTP request failed: {ex.Message}"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<T>
                {
                    Success = false,
                    ErrorMessage = $"Unexpected error: {ex.Message}"
                };
            }
        }

        internal ApiResponse<List<Entities.Task>> GetTasks(SearchFilter? filter)
        {
            string queryString = "tasks";
            if (filter is not null)
            {
                List<string> queryParams = [];
                if (!string.IsNullOrWhiteSpace(filter.Value.SearchString))
                {
                    queryParams.Add($"query={Uri.EscapeDataString(filter.Value.SearchString)}");
                }
                if (!filter.Value.IncludeCompleted)
                {
                    queryParams.Add("completed=false");
                }
                if (queryParams.Count > 0)
                {
                    queryString += "?" + string.Join("&", queryParams);
                }
            }
            return ExecuteWithHandling<List<Entities.Task>>(() =>
                _client.GetAsync(queryString).GetAwaiter().GetResult());
        }

        internal ApiResponse<Entities.Task> AddNewTask()
        {
            Entities.Task newTask = new()
            {
                Title = "New Task",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Tags = []
            };
            return ExecuteWithHandling<Entities.Task>(() =>
                _client.PostAsJsonAsync("tasks", newTask).GetAwaiter().GetResult());
        }
    }
}