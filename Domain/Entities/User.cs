using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Index(nameof(Email),IsUnique = true,Name = "Index_Email")]
[Index(nameof(FirstName),nameof(LastName),Name = "Index_FirstName_LastName")]
public record User
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("user_id")]
    [Key]
    public int Id { get; init; }

    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public bool Admin { get; init; }
    public required string Email { get; init; }
    
    public required string PasswordHash { get; set; }
    public required string PasswordSalt { get; set; }
    
    
}