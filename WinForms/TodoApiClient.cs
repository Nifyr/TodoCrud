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

        internal struct GetParams
        {
            public string? SearchString { get; set; }
            public bool IncludeCompleted { get; set; }
            public Entities.Task.SortingOptions? Sorting { get; set; }
        }

        internal struct ApiResponse<T>
        {
            public bool Success { get; set; }
            public T? Content { get; set; }
            public string? ErrorMessage { get; set; }
        }

        private static ApiResponse<T> ErrorResponse<T>(string? message) => new()
        {
            Success = false,
            ErrorMessage = message
        };

        private static ApiResponse<T> SuccessResponse<T>(T? content) => new()
        {
            Success = true,
            Content = content
        };

        // Method to get raw HttpResponseMessage with error handling
        private static ApiResponse<HttpResponseMessage> GetRawResponse(Func<HttpResponseMessage> requestFunc)
        {
            try
            {
                HttpResponseMessage response = requestFunc();
                if (!response.IsSuccessStatusCode)
                {
                    return ErrorResponse<HttpResponseMessage>($"API returned status code {response.StatusCode}");
                }
                return SuccessResponse(response);
            }
            catch (HttpRequestException ex)
            {
                return ErrorResponse<HttpResponseMessage>($"HTTP request failed: {ex.Message}");
            }
            catch (Exception ex)
            {
                return ErrorResponse<HttpResponseMessage>($"Unexpected error: {ex.Message}");
            }
        }

        // Generic method to execute API calls with error handling and deserialization
        private static ApiResponse<T> ExecuteWithHandling<T>(Func<HttpResponseMessage> requestFunc)
        {
            ApiResponse<HttpResponseMessage> rawResponse = GetRawResponse(requestFunc);
            if (!rawResponse.Success || rawResponse.Content is null)
            {
                return ErrorResponse<T>(rawResponse.ErrorMessage);
            }
            T? content = rawResponse.Content.Content.ReadFromJsonAsync<T>().Result;
            if (content is null)
            {
                return ErrorResponse<T>("Failed to deserialize API response");
            }
            return SuccessResponse(content);
        }

        // Overload for methods that don't return content (e.g., DELETE)
        private static ApiResponse<bool> ExecuteWithHandling(Func<HttpResponseMessage> requestFunc)
        {
            ApiResponse<HttpResponseMessage> rawResponse = GetRawResponse(requestFunc);
            if (!rawResponse.Success)
            {
                return ErrorResponse<bool>(rawResponse.ErrorMessage);
            }
            return SuccessResponse(true);
        }

        private static ApiResponse<T> InvalidTitleResponse<T>() =>
            ErrorResponse<T>($"Title must be between {Entities.Task.TitleMinLength} and {Entities.Task.TitleMaxLength} characters");

        internal ApiResponse<List<Entities.Task>> GetTasks(GetParams? filter)
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
                if (filter.Value.Sorting.HasValue)
                {
                    queryParams.Add($"sorting={filter.Value.Sorting.Value}");
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
            if (!Entities.Task.IsValidTitle(title))
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
            if (!updatedTask.HasValidTitle)
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