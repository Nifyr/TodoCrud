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

        public enum SortingOptions
        {
            IdAsc,
            IdDesc,
            TitleAsc,
            TitleDesc,
            DueDateAsc,
            DueDateDesc,
            CreatedAtAsc,
            CreatedAtDesc,
            UpdatedAtAsc,
            UpdatedAtDesc
        }

        public static IComparer<Task> GetComparer(SortingOptions option) => option switch
        {
            SortingOptions.IdAsc => Comparer<Task>.Create((a, b) => a.Id.CompareTo(b.Id)),
            SortingOptions.IdDesc => Comparer<Task>.Create((a, b) => b.Id.CompareTo(a.Id)),
            SortingOptions.TitleAsc => Comparer<Task>.Create((a, b) => string.Compare(a.Title, b.Title, StringComparison.OrdinalIgnoreCase)),
            SortingOptions.TitleDesc => Comparer<Task>.Create((a, b) => string.Compare(b.Title, a.Title, StringComparison.OrdinalIgnoreCase)),
            SortingOptions.DueDateAsc => Comparer<Task>.Create((a, b) =>
            {
                DateTime aDueDate = a.DueDate ?? DateTime.MaxValue;
                DateTime bDueDate = b.DueDate ?? DateTime.MaxValue;
                return aDueDate.CompareTo(bDueDate);
            }),
            SortingOptions.DueDateDesc => Comparer<Task>.Create((a, b) =>
            {
                DateTime aDueDate = a.DueDate ?? DateTime.MaxValue;
                DateTime bDueDate = b.DueDate ?? DateTime.MaxValue;
                return bDueDate.CompareTo(aDueDate);
            }),
            SortingOptions.CreatedAtAsc => Comparer<Task>.Create((a, b) => a.CreatedAt.CompareTo(b.CreatedAt)),
            SortingOptions.CreatedAtDesc => Comparer<Task>.Create((a, b) => b.CreatedAt.CompareTo(a.CreatedAt)),
            SortingOptions.UpdatedAtAsc => Comparer<Task>.Create((a, b) => a.UpdatedAt.CompareTo(b.UpdatedAt)),
            SortingOptions.UpdatedAtDesc => Comparer<Task>.Create((a, b) => b.UpdatedAt.CompareTo(a.UpdatedAt)),
            _ => throw new ArgumentOutOfRangeException(nameof(option), option, null)
        };

        public bool HasValidTitle { get => IsValidTitle(Title); }
        public static bool IsValidTitle(string title) =>
            title.Length >= TitleMinLength && title.Length <= TitleMaxLength;

        public override string ToString()
        {
            return Title;
        }
    }
}
