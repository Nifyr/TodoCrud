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

        internal ApiResponse<List<Entities.Task>> GetTasks(SearchFilter? filter)
        {
            try
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
                HttpResponseMessage response = _client.GetAsync(queryString).GetAwaiter().GetResult();
                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResponse<List<Entities.Task>>
                    {
                        Success = false,
                        ErrorMessage = $"API returned status code {response.StatusCode}"
                    };
                }
                List<Entities.Task>? tasks = response.Content.ReadFromJsonAsync<List<Entities.Task>>().Result;
                if (tasks is null)
                {
                    return new ApiResponse<List<Entities.Task>>
                    {
                        Success = false,
                        ErrorMessage = "Failed to deserialize API response"
                    };
                }
                return new ApiResponse<List<Entities.Task>>
                {
                    Success = true,
                    Content = tasks
                };
            }
            catch (HttpRequestException ex)
            {
                return new ApiResponse<List<Entities.Task>>
                {
                    Success = false,
                    ErrorMessage = $"HTTP request failed: {ex.Message}"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<Entities.Task>>
                {
                    Success = false,
                    ErrorMessage = $"Unexpected error: {ex.Message}"
                };
            }
        }
    }
}