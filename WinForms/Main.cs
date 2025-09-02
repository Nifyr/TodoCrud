using System.Net.Http.Json;
using TodoCrud.Entities;

namespace TodoCrud.WinForms
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();

            Populate(null);
        }

        private void Populate(string? searchQuery)
        {
            using HttpClient client = new();
            client.BaseAddress = new Uri("http://localhost:5185/");
            HttpResponseMessage response = client.GetAsync("tasks").Result;
            if (!response.IsSuccessStatusCode)
            {
                RetrieveFailureMessage();
                return;
            }

            List<Entities.Task>? tasks = response.Content.ReadFromJsonAsync<List<Entities.Task>>().Result;
            if (tasks is null)
            {
                RetrieveFailureMessage();
                return;
            }

            listBox1.DataSource = tasks;
        }

        private static void RetrieveFailureMessage()
        {
            MessageBox.Show("Failed to retrieve tasks from the API.");
        }
    }
}
