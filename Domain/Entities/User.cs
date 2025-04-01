using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class User
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("user_id")]
    public int Id { get; init; }

    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    
    public required string Email { get; init; }
    
    public required string PasswordHash { get; set; }
    public required string PasswordSalt { get; set; }
    
}