namespace TodoCrud.Entities
{
    public class Task
    {
        public const int TitleMinLength = 1;
        public const int TitleMaxLength = 140;

        public int Id { get; set; } // Primary key
        public string Title { get; set; } = string.Empty;
        public bool Completed { get; set; }
        public DateTime? DueDate { get; set; }
        public IEnumerable<string> Tags { get; set; } = [];
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public bool HasValidTitle { get => IsValidTitle(Title); }
        public static bool IsValidTitle(string title) =>
            title.Length >= TitleMinLength && title.Length <= TitleMaxLength;
        public override string ToString()
        {
            return Title;
        }
    }
}
