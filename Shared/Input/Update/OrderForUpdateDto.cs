using System.ComponentModel.DataAnnotations;

namespace Shared.Input.Update;

public record OrderForUpdateDto
{
    [Required]
    private OrderStatus Status { get; init; }
    
    int CourierId { get; init; }
}