namespace MauiToDoFinal.Api.Models
{
    public enum PriorityLevel
    {
        Düşük = 1,
        Orta = 2,
        Yüksek = 3
    }
    public class ToDoItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
        public string Category { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime DueDate { get; set; }
        public string CompletionText => IsCompleted ? "Yapıldı" : "Yapılmadı";
        public PriorityLevel Priority { get; set; } = PriorityLevel.Orta;
    }
}