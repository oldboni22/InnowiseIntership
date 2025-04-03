using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public record Courier
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("courier_id")]
    [Key]
    public int Id { get; init; }
    
    [Column("courier_name")]
    public required string Name { get; init; }
    
    public required string Vehicle { get; set; }
}