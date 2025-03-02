using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class User : Common
    {

        [Required]
        [MaxLength(50)]
        public required string Username { get; set; } // Required modifier ensures initialization

        [Required]
        [MaxLength(100)]
        public required string Email { get; set; }

        [Required]
        public required string PasswordHash { get; set; }
    }
}
