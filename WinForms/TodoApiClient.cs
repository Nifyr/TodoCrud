using System.Net.Http.Json;

namespace TodoCrud.WinForms
{
    internal class TodoApiClient()
    {
        private const string BASE_URL = "http://localhost:5185/";
        private const string PATH_TASKS = "tasks";

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

        // Method to get raw HttpResponseMessage with error handling
        private static ApiResponse<HttpResponseMessage> GetRawResponse(Func<HttpResponseMessage> requestFunc)
        {
            try
            {
                HttpResponseMessage response = requestFunc();
                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResponse<HttpResponseMessage>
                    {
                        Success = false,
                        ErrorMessage = $"API returned status code {response.StatusCode}"
                    };
                }
                return new ApiResponse<HttpResponseMessage>
                {
                    Success = true,
                    Content = response
                };
            }
            catch (HttpRequestException ex)
            {
                return new ApiResponse<HttpResponseMessage>
                {
                    Success = false,
                    ErrorMessage = $"HTTP request failed: {ex.Message}"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<HttpResponseMessage>
                {
                    Success = false,
                    ErrorMessage = $"Unexpected error: {ex.Message}"
                };
            }
        }

        // Generic method to execute API calls with error handling and deserialization
        private static ApiResponse<T> ExecuteWithHandling<T>(Func<HttpResponseMessage> requestFunc)
        {
            ApiResponse<HttpResponseMessage> rawResponse = GetRawResponse(requestFunc);
            if (!rawResponse.Success || rawResponse.Content is null)
            {
                return new ApiResponse<T>
                {
                    Success = false,
                    ErrorMessage = rawResponse.ErrorMessage
                };
            }
            T? content = rawResponse.Content.Content.ReadFromJsonAsync<T>().Result;
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

        // Overload for methods that don't return content (e.g., DELETE)
        private static ApiResponse<bool> ExecuteWithHandling(Func<HttpResponseMessage> requestFunc)
        {
            ApiResponse<HttpResponseMessage> rawResponse = GetRawResponse(requestFunc);
            if (!rawResponse.Success)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    ErrorMessage = rawResponse.ErrorMessage
                };
            }
            return new ApiResponse<bool>
            {
                Success = true,
                Content = true
            };
        }

        private static bool IsValidTitle(string title) =>
            title.Length >= Entities.Task.TitleMinLength &&
            title.Length <= Entities.Task.TitleMaxLength;

        private static ApiResponse<T> InvalidTitleResponse<T>() => new()
        {
            Success = false,
            ErrorMessage = $"Title must be between {Entities.Task.TitleMinLength} and {Entities.Task.TitleMaxLength} characters"
        };

        internal ApiResponse<List<Entities.Task>> GetTasks(SearchFilter? filter)
        {
            string queryString = PATH_TASKS;
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

        internal ApiResponse<Entities.Task> AddNewTask(string? title)
        {
            title ??= "New Task";
            if (!IsValidTitle(title))
            {
                return InvalidTitleResponse<Entities.Task>();
            }
            Entities.Task newTask = new()
            {
                Title = title,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Tags = []
            };
            return ExecuteWithHandling<Entities.Task>(() =>
                _client.PostAsJsonAsync(PATH_TASKS, newTask).GetAwaiter().GetResult());
        }

        internal ApiResponse<Entities.Task> UpdateTask(int id, Entities.Task updatedTask)
        {
            if (!IsValidTitle(updatedTask.Title))
            {
                return InvalidTitleResponse<Entities.Task>();
            }
            return ExecuteWithHandling<Entities.Task>(() =>
                _client.PutAsJsonAsync($"{PATH_TASKS}/{id}", updatedTask).GetAwaiter().GetResult());
        }

        internal ApiResponse<bool> DeleteTask(int id)
        {
            return ExecuteWithHandling(() =>
                _client.DeleteAsync($"{PATH_TASKS}/{id}").GetAwaiter().GetResult());
        }
    }
}