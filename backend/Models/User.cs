using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class User
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "TC Kimlik No must be 11 digits")]
    public string TCKimlikNo { get; set; }
    
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }
    
    [Required]
    public string Password { get; set; }
    
    public bool IsAdmin { get; set; } = false;
    
    public virtual ICollection<TaskItem> Tasks { get; set; }
} 