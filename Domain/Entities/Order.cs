using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public record Order
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("order_id")]
    public int Id { get; init; }
    
    public required string Status { get; set; }
    
    public DateTime CreatedAt { get; init; }
    public DateTime DeliveredAt { get; set; }
    
    public required string AddressHash { get; init; }
    public required string AddressSalt { get; init; }

    [ForeignKey(nameof(User))]
    public int UserId { get; init; }
    [ForeignKey(nameof(Courier))]
    public int CourierId { get; set; }

    public User? User { get; init; }
    public Courier? Courier { get; set; }
}