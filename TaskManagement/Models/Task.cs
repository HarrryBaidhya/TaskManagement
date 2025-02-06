namespace TaskManagement.Models
{
    public class Task
    {
        public string Id { get; set; } = "";
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string Status { get; set; } = "";
        public string DueDate { get; set; } = "";
        public string CreatedAt { get; set; } = "";
    }
}
