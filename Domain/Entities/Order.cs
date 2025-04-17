using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Domain.Entities;

[Index(nameof(CourierId),Name = "Index_CourierId")]
[Index(nameof(UserId),Name = "Index_UserId")]
public record Order
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("order_id")]
    [Key]
    public int Id { get; init; }
    
    public required OrderStatus OrderStatus { get; set; }
     
    public DateTime CreatedAt { get; init; }
    public DateTime DeliveredAt { get; set; }

    public required string Address { get; init; }
    public required string Description { get; init; }

    [ForeignKey(nameof(User))]
    public int UserId { get; init; }
    [ForeignKey(nameof(Courier))]
    public int CourierId { get; set; }

    public User? User { get; init; }
    public Courier? Courier { get; set; }
}