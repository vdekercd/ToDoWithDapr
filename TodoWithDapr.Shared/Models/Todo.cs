using System;

namespace ToDoWithDapr.Shared.Models
{

    public record Todo
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = String.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool IsDone { get; set; }
    }
}

