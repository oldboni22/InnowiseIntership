using System.ComponentModel.DataAnnotations;

namespace Shared.Input.Update;

public record OrderForUpdateDto : IValidatableObject
{
    [Required]
    private string Status { get; init; }
    
    int CourierId { get; init; }
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if(Status != "Pending" 
           && Status != "Shipped" 
           && Status != "Delivered" 
           && Status != "Cancelled")
            yield return new ValidationResult("Wrong status", [nameof(Status)]);
            
    }
}