using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Index(nameof(Name),Name = "Index_Name")]
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