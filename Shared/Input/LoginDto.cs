using System.ComponentModel.DataAnnotations;

namespace Shared.Input;

public record LoginDto
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email is invalid")]
    [MaxLength(50, ErrorMessage = "Email must be less than 50 characters")]
    public string Email {get;init;}

    [Required(ErrorMessage = "Password is required")]
    [MaxLength(50, ErrorMessage = "Password must be less than 50 characters")]
    public string Password{get;init;}
    
};