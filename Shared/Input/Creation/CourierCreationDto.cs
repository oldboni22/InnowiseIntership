using System.ComponentModel.DataAnnotations;

namespace Shared.Input.Creation;

public record CourierCreationDto
{
    [Required(ErrorMessage = "Name is required")]
    [MaxLength(35,ErrorMessage = "Name must be less than 35 characters")]
    public string Name { get; init; }
    
    [Required(ErrorMessage = "Vehicle is  required")]
    [MaxLength(25, ErrorMessage = "Vehicle must be less than 25 characters")]
    public string Vehicle { get; init; }
};