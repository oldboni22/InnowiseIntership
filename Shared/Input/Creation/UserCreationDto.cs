using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Shared.Input.Creation;

public record UserCreationDto 
{
    [Required(ErrorMessage = "First name is required")]
    [MaxLength(50, ErrorMessage = "First name must be less than 50 characters")]
    public string FirstName {get;init;}
    
    
    [Required(ErrorMessage = "Last name is required")]
    [MaxLength(50, ErrorMessage = "Last name must be less than 50 characters")]
    public string LastName {get;init;}
    
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email is not valid")]
    [MaxLength(50, ErrorMessage = "Email must be less than 50 characters")]
    public string Email {get;init;}
    
    [Required(ErrorMessage = "Password is required")]
    [MaxLength(50, ErrorMessage = "Password must be less than 50 characters")]
    public string Password {get;init;}
    
    [DefaultValue(false)]
    public bool Admin {get;init;}
    
};