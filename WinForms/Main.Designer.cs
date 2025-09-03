namespace TodoCrud.WinForms
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanel1 = new TableLayoutPanel();
            panel1 = new Panel();
            tableLayoutPanel2 = new TableLayoutPanel();
            textBox1 = new TextBox();
            button1 = new Button();
            deleteTaskButton = new Button();
            addTaskButton = new Button();
            button2 = new Button();
            showCompletedCheckBox = new CheckBox();
            taskListBox = new ListBox();
            panel2 = new Panel();
            tableLayoutPanel3 = new TableLayoutPanel();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            idTextBox = new TextBox();
            titleTextBox = new TextBox();
            completedCheckBox = new CheckBox();
            createdAtDateTimePicker = new DateTimePicker();
            updatedAtDateTimePicker3 = new DateTimePicker();
            dueDateDateTimePicker = new DateTimePicker();
            dueDateCheckBox = new CheckBox();
            tableLayoutPanel4 = new TableLayoutPanel();
            tagsListBox = new ListBox();
            tableLayoutPanel5 = new TableLayoutPanel();
            tagTextBox = new TextBox();
            removeTagButton = new Button();
            addTagButton = new Button();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            panel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80F));
            tableLayoutPanel1.Controls.Add(panel1, 0, 0);
            tableLayoutPanel1.Controls.Add(taskListBox, 0, 1);
            tableLayoutPanel1.Controls.Add(panel2, 1, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(800, 450);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(panel1, 2);
            panel1.Controls.Add(tableLayoutPanel2);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(794, 31);
            panel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoSize = true;
            tableLayoutPanel2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel2.ColumnCount = 6;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.Controls.Add(textBox1, 0, 0);
            tableLayoutPanel2.Controls.Add(button1, 1, 0);
            tableLayoutPanel2.Controls.Add(deleteTaskButton, 5, 0);
            tableLayoutPanel2.Controls.Add(addTaskButton, 4, 0);
            tableLayoutPanel2.Controls.Add(button2, 3, 0);
            tableLayoutPanel2.Controls.Add(showCompletedCheckBox, 2, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(0, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.Size = new Size(794, 31);
            tableLayoutPanel2.TabIndex = 2;
            // 
            // textBox1
            // 
            textBox1.Dock = DockStyle.Fill;
            textBox1.Location = new Point(3, 3);
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "Search tasks...";
            textBox1.Size = new Size(446, 23);
            textBox1.TabIndex = 0;
            // 
            // button1
            // 
            button1.AutoSize = true;
            button1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            button1.Location = new Point(455, 3);
            button1.Name = "button1";
            button1.Size = new Size(52, 25);
            button1.TabIndex = 1;
            button1.Text = "Search";
            button1.UseVisualStyleBackColor = true;
            // 
            // deleteTaskButton
            // 
            deleteTaskButton.AutoSize = true;
            deleteTaskButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            deleteTaskButton.Location = new Point(741, 3);
            deleteTaskButton.Name = "deleteTaskButton";
            deleteTaskButton.Size = new Size(50, 25);
            deleteTaskButton.TabIndex = 4;
            deleteTaskButton.Text = "Delete";
            deleteTaskButton.UseVisualStyleBackColor = true;
            deleteTaskButton.Click += DeleteTaskButton_Click;
            // 
            // addTaskButton
            // 
            addTaskButton.AutoSize = true;
            addTaskButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            addTaskButton.Location = new Point(696, 3);
            addTaskButton.Name = "addTaskButton";
            addTaskButton.Size = new Size(39, 25);
            addTaskButton.TabIndex = 3;
            addTaskButton.Text = "Add";
            addTaskButton.UseVisualStyleBackColor = true;
            addTaskButton.Click += AddTaskButton_Click;
            // 
            // button2
            // 
            button2.AutoSize = true;
            button2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            button2.Location = new Point(634, 3);
            button2.Name = "button2";
            button2.Size = new Size(56, 25);
            button2.TabIndex = 2;
            button2.Text = "Refresh";
            button2.UseVisualStyleBackColor = true;
            // 
            // showCompletedCheckBox
            // 
            showCompletedCheckBox.Anchor = AnchorStyles.Left;
            showCompletedCheckBox.AutoSize = true;
            showCompletedCheckBox.Location = new Point(513, 6);
            showCompletedCheckBox.Name = "showCompletedCheckBox";
            showCompletedCheckBox.Size = new Size(115, 19);
            showCompletedCheckBox.TabIndex = 5;
            showCompletedCheckBox.Text = "Show completed";
            showCompletedCheckBox.UseVisualStyleBackColor = true;
            // 
            // taskListBox
            // 
            taskListBox.Dock = DockStyle.Fill;
            taskListBox.FormattingEnabled = true;
            taskListBox.ItemHeight = 15;
            taskListBox.Location = new Point(3, 40);
            taskListBox.Name = "taskListBox";
            taskListBox.Size = new Size(154, 407);
            taskListBox.TabIndex = 1;
            taskListBox.SelectedIndexChanged += TaskListBox_SelectedIndexChanged;
            // 
            // panel2
            // 
            panel2.Controls.Add(tableLayoutPanel3);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(163, 40);
            panel2.Name = "panel2";
            panel2.Size = new Size(634, 407);
            panel2.TabIndex = 2;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 3;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(label1, 0, 0);
            tableLayoutPanel3.Controls.Add(label2, 0, 1);
            tableLayoutPanel3.Controls.Add(label3, 0, 2);
            tableLayoutPanel3.Controls.Add(label4, 0, 3);
            tableLayoutPanel3.Controls.Add(label5, 0, 4);
            tableLayoutPanel3.Controls.Add(label6, 0, 5);
            tableLayoutPanel3.Controls.Add(label7, 0, 6);
            tableLayoutPanel3.Controls.Add(idTextBox, 1, 0);
            tableLayoutPanel3.Controls.Add(titleTextBox, 1, 1);
            tableLayoutPanel3.Controls.Add(completedCheckBox, 1, 2);
            tableLayoutPanel3.Controls.Add(createdAtDateTimePicker, 1, 4);
            tableLayoutPanel3.Controls.Add(updatedAtDateTimePicker3, 1, 5);
            tableLayoutPanel3.Controls.Add(dueDateDateTimePicker, 2, 3);
            tableLayoutPanel3.Controls.Add(dueDateCheckBox, 1, 3);
            tableLayoutPanel3.Controls.Add(tableLayoutPanel4, 1, 6);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(0, 0);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 7;
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new Size(634, 407);
            tableLayoutPanel3.TabIndex = 0;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Location = new Point(33, 7);
            label1.Name = "label1";
            label1.Size = new Size(17, 15);
            label1.TabIndex = 0;
            label1.Text = "Id";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.None;
            label2.AutoSize = true;
            label2.Location = new Point(27, 36);
            label2.Name = "label2";
            label2.Size = new Size(30, 15);
            label2.TabIndex = 1;
            label2.Text = "Title";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.None;
            label3.AutoSize = true;
            label3.Location = new Point(9, 60);
            label3.Name = "label3";
            label3.Size = new Size(66, 15);
            label3.TabIndex = 2;
            label3.Text = "Completed";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.None;
            label4.AutoSize = true;
            label4.Location = new Point(15, 85);
            label4.Name = "label4";
            label4.Size = new Size(54, 15);
            label4.TabIndex = 3;
            label4.Text = "Due date";
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.None;
            label5.AutoSize = true;
            label5.Location = new Point(3, 114);
            label5.Name = "label5";
            label5.Size = new Size(78, 15);
            label5.TabIndex = 4;
            label5.Text = "Creation date";
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.None;
            label6.AutoSize = true;
            label6.Location = new Point(8, 143);
            label6.Name = "label6";
            label6.Size = new Size(68, 15);
            label6.TabIndex = 5;
            label6.Text = "Last update";
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.None;
            label7.AutoSize = true;
            label7.Location = new Point(26, 278);
            label7.Name = "label7";
            label7.Size = new Size(31, 15);
            label7.TabIndex = 6;
            label7.Text = "Tags";
            // 
            // idTextBox
            // 
            tableLayoutPanel3.SetColumnSpan(idTextBox, 2);
            idTextBox.Dock = DockStyle.Fill;
            idTextBox.Enabled = false;
            idTextBox.Location = new Point(87, 3);
            idTextBox.Name = "idTextBox";
            idTextBox.Size = new Size(544, 23);
            idTextBox.TabIndex = 7;
            // 
            // titleTextBox
            // 
            tableLayoutPanel3.SetColumnSpan(titleTextBox, 2);
            titleTextBox.Dock = DockStyle.Fill;
            titleTextBox.Enabled = false;
            titleTextBox.Location = new Point(87, 32);
            titleTextBox.Name = "titleTextBox";
            titleTextBox.Size = new Size(544, 23);
            titleTextBox.TabIndex = 8;
            // 
            // completedCheckBox
            // 
            completedCheckBox.Anchor = AnchorStyles.Left;
            completedCheckBox.AutoSize = true;
            tableLayoutPanel3.SetColumnSpan(completedCheckBox, 2);
            completedCheckBox.Enabled = false;
            completedCheckBox.Location = new Point(87, 61);
            completedCheckBox.Name = "completedCheckBox";
            completedCheckBox.Size = new Size(15, 14);
            completedCheckBox.TabIndex = 9;
            completedCheckBox.UseVisualStyleBackColor = true;
            // 
            // createdAtDateTimePicker
            // 
            tableLayoutPanel3.SetColumnSpan(createdAtDateTimePicker, 2);
            createdAtDateTimePicker.Dock = DockStyle.Fill;
            createdAtDateTimePicker.Enabled = false;
            createdAtDateTimePicker.Location = new Point(87, 110);
            createdAtDateTimePicker.Name = "createdAtDateTimePicker";
            createdAtDateTimePicker.Size = new Size(544, 23);
            createdAtDateTimePicker.TabIndex = 11;
            // 
            // updatedAtDateTimePicker3
            // 
            tableLayoutPanel3.SetColumnSpan(updatedAtDateTimePicker3, 2);
            updatedAtDateTimePicker3.Dock = DockStyle.Fill;
            updatedAtDateTimePicker3.Enabled = false;
            updatedAtDateTimePicker3.Location = new Point(87, 139);
            updatedAtDateTimePicker3.Name = "updatedAtDateTimePicker3";
            updatedAtDateTimePicker3.Size = new Size(544, 23);
            updatedAtDateTimePicker3.TabIndex = 12;
            // 
            // dueDateDateTimePicker
            // 
            dueDateDateTimePicker.Dock = DockStyle.Fill;
            dueDateDateTimePicker.Enabled = false;
            dueDateDateTimePicker.Location = new Point(107, 81);
            dueDateDateTimePicker.Name = "dueDateDateTimePicker";
            dueDateDateTimePicker.Size = new Size(524, 23);
            dueDateDateTimePicker.TabIndex = 10;
            // 
            // dueDateCheckBox
            // 
            dueDateCheckBox.Anchor = AnchorStyles.Left;
            dueDateCheckBox.AutoSize = true;
            dueDateCheckBox.Enabled = false;
            dueDateCheckBox.Location = new Point(87, 83);
            dueDateCheckBox.Name = "dueDateCheckBox";
            dueDateCheckBox.Size = new Size(14, 19);
            dueDateCheckBox.TabIndex = 13;
            dueDateCheckBox.Text = "checkBox2";
            dueDateCheckBox.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 2;
            tableLayoutPanel3.SetColumnSpan(tableLayoutPanel4, 2);
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel4.Controls.Add(tagsListBox, 0, 0);
            tableLayoutPanel4.Controls.Add(tableLayoutPanel5, 1, 0);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(87, 168);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 1;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel4.Size = new Size(544, 236);
            tableLayoutPanel4.TabIndex = 14;
            // 
            // tagsListBox
            // 
            tagsListBox.Dock = DockStyle.Fill;
            tagsListBox.FormattingEnabled = true;
            tagsListBox.ItemHeight = 15;
            tagsListBox.Location = new Point(3, 3);
            tagsListBox.Name = "tagsListBox";
            tagsListBox.Size = new Size(266, 230);
            tagsListBox.TabIndex = 0;
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.ColumnCount = 2;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel5.Controls.Add(tagTextBox, 0, 0);
            tableLayoutPanel5.Controls.Add(removeTagButton, 0, 2);
            tableLayoutPanel5.Controls.Add(addTagButton, 0, 1);
            tableLayoutPanel5.Dock = DockStyle.Fill;
            tableLayoutPanel5.Location = new Point(275, 3);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 4;
            tableLayoutPanel5.RowStyles.Add(new RowStyle());
            tableLayoutPanel5.RowStyles.Add(new RowStyle());
            tableLayoutPanel5.RowStyles.Add(new RowStyle());
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel5.Size = new Size(266, 230);
            tableLayoutPanel5.TabIndex = 1;
            // 
            // tagTextBox
            // 
            tableLayoutPanel5.SetColumnSpan(tagTextBox, 2);
            tagTextBox.Dock = DockStyle.Fill;
            tagTextBox.Enabled = false;
            tagTextBox.Location = new Point(3, 3);
            tagTextBox.Name = "tagTextBox";
            tagTextBox.PlaceholderText = "New tag...";
            tagTextBox.Size = new Size(260, 23);
            tagTextBox.TabIndex = 0;
            // 
            // removeTagButton
            // 
            removeTagButton.AutoSize = true;
            removeTagButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            removeTagButton.Enabled = false;
            removeTagButton.Location = new Point(3, 63);
            removeTagButton.Name = "removeTagButton";
            removeTagButton.Size = new Size(106, 25);
            removeTagButton.TabIndex = 1;
            removeTagButton.Text = "Remove selected";
            removeTagButton.UseVisualStyleBackColor = true;
            // 
            // addTagButton
            // 
            addTagButton.AutoSize = true;
            addTagButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            addTagButton.Enabled = false;
            addTagButton.Location = new Point(3, 32);
            addTagButton.Name = "addTagButton";
            addTagButton.Size = new Size(64, 25);
            addTagButton.TabIndex = 2;
            addTagButton.Text = "Add new";
            addTagButton.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tableLayoutPanel1);
            Name = "Main";
            Text = "Todo List";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            panel2.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel5.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel1;
        private ListBox taskListBox;
        private Panel panel2;
        private TextBox textBox1;
        private Button button1;
        private TableLayoutPanel tableLayoutPanel2;
        private Button button2;
        private Button addTaskButton;
        private Button deleteTaskButton;
        private TableLayoutPanel tableLayoutPanel3;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private TextBox idTextBox;
        private TextBox titleTextBox;
        private CheckBox completedCheckBox;
        private DateTimePicker dueDateDateTimePicker;
        private DateTimePicker createdAtDateTimePicker;
        private DateTimePicker updatedAtDateTimePicker3;
        private CheckBox dueDateCheckBox;
        private TableLayoutPanel tableLayoutPanel4;
        private ListBox tagsListBox;
        private TableLayoutPanel tableLayoutPanel5;
        private TextBox tagTextBox;
        private Button removeTagButton;
        private Button addTagButton;
        private CheckBox showCompletedCheckBox;
    }
}
