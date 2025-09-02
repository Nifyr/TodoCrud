using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
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

        struct SearchFilter
        {
            public string SearchString { get; set; }
            public bool IncludeCompleted { get; set; }
        }

        private void Populate(SearchFilter? filter)
        {
            using HttpClient client = new();
            client.BaseAddress = new Uri("http://localhost:5185/");
            HttpResponseMessage response;
            try
            {
                response = client.GetAsync("tasks").GetAwaiter().GetResult();
            }
            catch (HttpRequestException)
            {
                // Could not reach the API
                RetrieveFailureMessage();
                return;
            }

            if (!response.IsSuccessStatusCode)
            {
                // Received response but not successful
                RetrieveFailureMessage();
                return;
            }

            List<Entities.Task>? tasks = response.Content.ReadFromJsonAsync<List<Entities.Task>>().Result;
            if (tasks is null)
            {
                // Response content could not be deserialized
                RetrieveFailureMessage();
                return;
            }
            taskListBox.DataSource = tasks;
        }

        private static void RetrieveFailureMessage()
        {
            MessageBox.Show("Failed to retrieve tasks from the API.");
        }

        private void TaskListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTaskDisplay();
        }

        private void UpdateTaskDisplay()
        {
            int selectedIndex = taskListBox.SelectedIndex;
            DisableTaskInputControls();
            if (selectedIndex < 0)
            {
                // No item is selected
                ClearTaskDisplay();
                return;
            }
            Entities.Task task = (Entities.Task)taskListBox.Items[selectedIndex];

            idTextBox.Text = task.Id.ToString();

            titleTextBox.Text = task.Title;
            titleTextBox.Enabled = true;

            completedCheckBox.Checked = task.Completed;
            completedCheckBox.Enabled = true;

            dueDateCheckBox.Checked = task.DueDate != null;
            dueDateCheckBox.Enabled = true;
            if (task.DueDate is null)
            {
                // No due date set
                dueDateDateTimePicker.Value = DateTime.Now;
                dueDateDateTimePicker.Visible = false;
                dueDateDateTimePicker.Enabled = false;
            }
            else
            {
                dueDateDateTimePicker.Value = task.DueDate.Value;
                dueDateDateTimePicker.Visible = true;
                dueDateDateTimePicker.Enabled = true;
            }

            createdAtDateTimePicker.Value = task.CreatedAt;

            updatedAtDateTimePicker3.Value = task.UpdatedAt;

            tagsListBox.DataSource = task.Tags.ToList();
            tagsListBox.Enabled = true;

            tagTextBox.Text = string.Empty;
            tagTextBox.Enabled = true;

            removeTagButton.Enabled = true;

            addTagButton.Enabled = true;
        }

        private void DisableTaskInputControls()
        {
            idTextBox.Enabled = false;
            titleTextBox.Enabled = false;
            completedCheckBox.Enabled = false;
            dueDateCheckBox.Enabled = false;
            dueDateDateTimePicker.Enabled = false;
            createdAtDateTimePicker.Enabled = false;
            updatedAtDateTimePicker3.Enabled = false;
            tagsListBox.Enabled = false;
            removeTagButton.Enabled = false;
            addTagButton.Enabled = false;
        }

        private void ClearTaskDisplay()
        {
            idTextBox.Text = string.Empty;
            titleTextBox.Text = string.Empty;
            completedCheckBox.Checked = false;
            dueDateCheckBox.Checked = false;
            dueDateDateTimePicker.Value = DateTime.Now;
            createdAtDateTimePicker.Value = DateTime.Now;
            updatedAtDateTimePicker3.Value = DateTime.Now;
            tagsListBox.DataSource = null;
            tagTextBox.Text = string.Empty;
        }
    }
}
