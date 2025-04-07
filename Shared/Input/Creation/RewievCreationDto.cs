using System.ComponentModel.DataAnnotations;

namespace Shared.Input.Creation;

public record ReviewCreationDto
{
    [Range(1,10, ErrorMessage = "Rating must be in range between 1 and 10")]
    public int Rating{get ;init;}
    
    [MaxLength(500, ErrorMessage = "Comment must be less than 500 characters")]
    public string Comment{get;init;}
    
};