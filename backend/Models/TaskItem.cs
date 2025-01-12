using System;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; } = string.Empty;
        
        public string? Description { get; set; }
        
        public bool IsCompleted { get; set; } = false;
        
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        
        public DateTime? CompletedDate { get; set; }

        [Required]
        public int UserId { get; set; }
        
        public virtual User User { get; set; }
    }
}