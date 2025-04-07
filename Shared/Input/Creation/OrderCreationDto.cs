using System.ComponentModel.DataAnnotations;

namespace Shared.Input.Creation;

public record OrderCreationDto
{
    [MaxLength(500, ErrorMessage = "Comment must be less than 500 characters")]
    public string Comment { get; init; }
    
    [Required(ErrorMessage =  "Address is required")]
    [MaxLength(50, ErrorMessage = "Address must be less than 50 characters")]
    public string Address { get; init; }
}