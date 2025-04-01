using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Courier
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("courier_id")]
    public int Id { get; init; }
    
    [Column("courier_name")]
    public required string Name { get; init; }
    
    public required string Vehicle { get; set; }
}