using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public record Review
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("rating_id")]
    public int Id { get; init; }
    
    public DateTime LastEditedAt { get; set; }
    public required string Comment { get; init; }
    [MaxLength(10)]
    public int Rating { get; init; }
    [ForeignKey(nameof(User))]
    public int UserId { get; init; }
    
}