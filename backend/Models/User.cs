using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public int Id { get; set; } // Primary Key

    [Required]
    [MaxLength(50)]
    public string Username { get; set; } // Unique username

    [Required]
    [MaxLength(100)]
    public string Email { get; set; } // Unique email address

    [Required]
    public string PasswordHash { get; set; } // Hashed password
}
