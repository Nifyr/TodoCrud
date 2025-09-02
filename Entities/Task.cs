namespace TodoCrud.Entities
{
    public class Task
    {
        public int Id { get; set; } // Primary key
        public string Title { get; set; } = string.Empty; // min length 1, max length 140
        public bool Completed { get; set; }
        public DateTime? DueDate { get; set; }
        public IEnumerable<string> Tags { get; set; } = [];
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}
