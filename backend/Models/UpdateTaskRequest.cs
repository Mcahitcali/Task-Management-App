using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class UpdateTaskRequest
    {
        [StringLength(200, MinimumLength = 1)]
        public string? Title { get; set; }
        
        public string? Description { get; set; }
        
        public bool IsCompleted { get; set; }
    }
} 