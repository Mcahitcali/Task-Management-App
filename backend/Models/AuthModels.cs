using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class LoginRequest
{
    [Required]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "TC Kimlik No must be 11 digits")]
    public string TCKimlikNo { get; set; }
    
    [Required]
    public string Password { get; set; }
}

public class RegisterRequest
{
    [Required]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "TC Kimlik No must be 11 digits")]
    public string TCKimlikNo { get; set; }
    
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }
    
    [Required]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
    public string Password { get; set; }
}

public class AuthResponse
{
    public string Token { get; set; }
    public string TCKimlikNo { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool IsAdmin { get; set; }
} 