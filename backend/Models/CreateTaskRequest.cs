using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class CreateTaskRequest
    {
        [Required]
        [StringLength(200, MinimumLength = 1)]
        public string Title { get; set; }
        
        [Required]
        public string Description { get; set; }
    }
} 