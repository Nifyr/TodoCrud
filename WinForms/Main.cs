namespace TodoCrud.WinForms
{
    public partial class Main : Form
    {
        private readonly TodoApiClient apiClient = new();

        public Main()
        {
            InitializeComponent();

            sortingOptionComboBox.DataSource = Enum.GetValues(typeof(Entities.Task.SortingOptions))
                .Cast<Entities.Task.SortingOptions>()
                .Select(o => o.GetDisplayTitle())
                .ToList();

            RefreshTaskList();
        }

        private void RefreshTaskList()
        {
            int lastSelectedTaskId = -1;
            if (taskListBox.SelectedItem is Entities.Task selectedTask)
            {
                lastSelectedTaskId = selectedTask.Id;
            }
            TodoApiClient.SearchFilter filter = new()
            {
                SearchString = searchTextBox.Text,
                IncludeCompleted = showCompletedCheckBox.Checked
            };
            apiClient.GetTasks(filter).Handle(tasks =>
            {
                taskListBox.SelectedIndexChanged -= TaskListBox_SelectedIndexChanged!;
                taskListBox.DataSource = tasks;
                if (tasks.Count > 0)
                {
                    // Try to reselect the previously selected task, if it still exists
                    int indexToSelect = 0;
                    if (lastSelectedTaskId != -1)
                    {
                        indexToSelect = tasks.FindIndex(t => t.Id == lastSelectedTaskId);
                        if (indexToSelect == -1)
                        {
                            indexToSelect = 0;
                        }
                    }
                    taskListBox.SelectedIndex = indexToSelect;
                    if (taskListBox.SelectedItem is Entities.Task task && task.Id != lastSelectedTaskId)
                    {
                        UpdateTaskDisplay();
                    }
                }
                else
                {
                    DisableTaskInputControls();
                    ClearTaskDisplay();
                }
                taskListBox.SelectedIndexChanged += TaskListBox_SelectedIndexChanged!;
            });
        }

        private void TaskListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTaskDisplay();
        }

        private void UpdateTaskDisplay()
        {
            int selectedIndex = taskListBox.SelectedIndex;
            DisableTaskInputControls();
            DisableTaskUpdateEventHandlers();
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
                dueDateDateTimePicker.Enabled = false;
                dueDateDateTimePicker.Visible = false;
                dueDateDateTimePicker.Value = DateTime.Now;
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

            EnableTaskUpdateEventHandlers();
        }

        private void EnableTaskUpdateEventHandlers()
        {
            titleTextBox.KeyDown += TitleTextBox_KeyDown!;
            completedCheckBox.CheckedChanged += SimpleUpdateEventHandler!;
            dueDateCheckBox.CheckedChanged += DueDateCheckBox_CheckedChanged!;
            dueDateDateTimePicker.ValueChanged += SimpleUpdateEventHandler!;
            tagTextBox.KeyDown += TagTextBox_KeyDown!;
            addTagButton.Click += AddTagButton_Click!;
            removeTagButton.Click += RemoveTagButton_Click!;
        }

        private void DisableTaskUpdateEventHandlers()
        {
            titleTextBox.KeyDown -= TitleTextBox_KeyDown!;
            completedCheckBox.CheckedChanged -= SimpleUpdateEventHandler!;
            dueDateCheckBox.CheckedChanged -= DueDateCheckBox_CheckedChanged!;
            dueDateDateTimePicker.ValueChanged -= SimpleUpdateEventHandler!;
            tagTextBox.KeyDown -= TagTextBox_KeyDown!;
            addTagButton.Click -= AddTagButton_Click!;
            removeTagButton.Click -= RemoveTagButton_Click!;
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
            tagTextBox.Enabled = false;
            removeTagButton.Enabled = false;
            addTagButton.Enabled = false;
        }

        private void ClearTaskDisplay()
        {
            idTextBox.Text = string.Empty;
            titleTextBox.Text = string.Empty;
            completedCheckBox.Checked = false;
            dueDateCheckBox.Checked = false;
            dueDateDateTimePicker.Visible = false;
            dueDateDateTimePicker.Value = DateTime.Now;
            createdAtDateTimePicker.Value = DateTime.Now;
            updatedAtDateTimePicker3.Value = DateTime.Now;
            tagsListBox.DataSource = null;
            tagTextBox.Text = string.Empty;
        }

        private void AddTaskButton_Click(object sender, EventArgs e)
        {
            apiClient.AddNewTask(null).Handle(task =>
            {
                IEnumerable<Entities.Task>? newTasks = [];
                if (taskListBox.DataSource is IEnumerable<Entities.Task> tasks)
                {
                    newTasks = tasks;
                }
                taskListBox.DataSource = newTasks.Append(task).ToList();
                taskListBox.SelectedItem = task;
            });
        }

        private void DeleteTaskButton_Click(object sender, EventArgs e)
        {
            if (taskListBox.SelectedItem is not Entities.Task task)
            {
                MessageBox.Show("No task selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DialogResult result = MessageBox.Show($"Are you sure you want to delete the task \"{task.Title}\"?",
                "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result != DialogResult.Yes)
                return;
            apiClient.DeleteTask(task.Id).Handle(_ =>
            {
                RefreshTaskList();
            });
        }

        private void UpdateTask()
        {
            if (taskListBox.SelectedItem is not Entities.Task task)
                return;
            Entities.Task updatedTask = new()
            {
                Title = titleTextBox.Text,
                Completed = completedCheckBox.Checked,
                DueDate = dueDateCheckBox.Checked ? dueDateDateTimePicker.Value : null,
                Tags = [.. tagsListBox.Items.Cast<string>()]
            };
            apiClient.UpdateTask(task.Id, updatedTask).Handle(updatedTask => {
                task.Id = updatedTask.Id;
                task.Title = updatedTask.Title;
                task.Completed = updatedTask.Completed;
                task.DueDate = updatedTask.DueDate;
                task.Tags = updatedTask.Tags;
                task.CreatedAt = updatedTask.CreatedAt;
                task.UpdatedAt = updatedTask.UpdatedAt;
            });
        }

        private void TitleTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            // Prevent the ding sound
            e.Handled = true;
            e.SuppressKeyPress = true;

            UpdateTask();

            if (sender is not TextBox textBox)
                return;
            textBox.SelectionStart = textBox.Text.Length;
            textBox.SelectionLength = 0;
            textBox.Focus();
        }

        private void SimpleUpdateEventHandler(object sender, EventArgs e)
        {
            UpdateTask();
        }

        private void DueDateCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            dueDateDateTimePicker.ValueChanged -= SimpleUpdateEventHandler!;
            dueDateDateTimePicker.Enabled = dueDateCheckBox.Checked;
            dueDateDateTimePicker.Visible = dueDateCheckBox.Checked;
            dueDateDateTimePicker.Value = DateTime.Now;
            dueDateDateTimePicker.ValueChanged += SimpleUpdateEventHandler!;
            UpdateTask();
        }

        private void AddTag()
        {
            string newTag = tagTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(newTag))
                return;
            List<string> newTags = [.. tagsListBox.Items.Cast<string>()];
            newTags.Add(newTag);
            tagsListBox.DataSource = newTags;
            tagTextBox.Text = string.Empty;
            UpdateTask();
        }

        private void TagTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            // Prevent the ding sound
            e.Handled = true;
            e.SuppressKeyPress = true;

            AddTag();

            if (sender is not TextBox textBox)
                return;
            textBox.Focus();
        }

        private void AddTagButton_Click(object sender, EventArgs e)
        {
            AddTag();
        }

        private void RemoveTagButton_Click(object sender, EventArgs e)
        {
            int selectedIndex = tagsListBox.SelectedIndex;
            if (selectedIndex < 0)
                return;
            List<string> newTags = [.. tagsListBox.Items.Cast<string>()];
            newTags.RemoveAt(selectedIndex);
            tagsListBox.DataSource = newTags;
            UpdateTask();
        }

        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            // Prevent the ding sound
            e.Handled = true;
            e.SuppressKeyPress = true;

            RefreshTaskList();
        }

        private void SimpleSearchEventHandler(object sender, EventArgs e)
        {
            RefreshTaskList();
        }
    }

    internal static class Extensions
    {
        // Helper method to handle API responses. Gotta love functional programming.
        internal static bool Handle<T>(this TodoApiClient.ApiResponse<T> response, Action<T>? onSuccess)
        {
            if (!response.Success || response.Content is null)
            {
                string? errorMessage = response.ErrorMessage;
                errorMessage ??= "Unknown error.";
                MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (onSuccess is not null)
                onSuccess(response.Content);
            return true;
        }

        internal static string GetDisplayTitle(this Entities.Task.SortingOptions sortingOptions) =>
            sortingOptions switch
            {
                Entities.Task.SortingOptions.IdAsc => "ID Ascending",
                Entities.Task.SortingOptions.IdDesc => "ID Descending",
                Entities.Task.SortingOptions.TitleAsc => "Title A-Z",
                Entities.Task.SortingOptions.TitleDesc => "Title Z-A",
                Entities.Task.SortingOptions.DueDateAsc => "Due Date Asc",
                Entities.Task.SortingOptions.DueDateDesc => "Due Date Desc",
                Entities.Task.SortingOptions.CreatedAtAsc => "Creation Date Asc",
                Entities.Task.SortingOptions.CreatedAtDesc => "Creation Date Desc",
                Entities.Task.SortingOptions.UpdatedAtAsc => "Last Update Asc",
                Entities.Task.SortingOptions.UpdatedAtDesc => "Last Update Desc",
                _ => sortingOptions.ToString() ?? "Unknown"
            };
    }
}
