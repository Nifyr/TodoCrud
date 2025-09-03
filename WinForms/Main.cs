namespace TodoCrud.WinForms
{
    public partial class Main : Form
    {
        private readonly TodoApiClient apiClient = new();

        public Main()
        {
            InitializeComponent();

            Populate(null);
        }

        private void Populate(TodoApiClient.SearchFilter? filter)
        {
            TodoApiClient.ApiResponse<List<Entities.Task>> response = apiClient.GetTasks(filter);
            if (!response.Success || response.Content is null)
        {
                FailureMessage(response.ErrorMessage);
                return;
            }
            taskListBox.DataSource = response.Content;
            }

        private static void FailureMessage(string? errorMessage)
            {
            errorMessage ??= "Unknown error.";
            MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
