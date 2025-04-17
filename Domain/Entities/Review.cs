using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Index(nameof(CourierId),Name = "Index_CourierId")]
[Index(nameof(UserId),Name = "Index_UserId")]
[Index(nameof(Rating),Name = "Index_Rating",AllDescending = false)]
public record Review
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("rating_id")]
    [Key]
    public int Id { get; init; }
    
    public DateTime PostedAt { get; set; }
    public required string Comment { get; init; }
    [MaxLength(10)]
    public int Rating { get; init; }
    
    [ForeignKey(nameof(User))]
    public int UserId { get; init; }
    [ForeignKey(nameof(Courier))]
    public int CourierId { get; init; }

    public required User User { get; init; }
    public required Courier Courier { get; init; }

}