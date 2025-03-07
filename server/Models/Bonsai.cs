using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public class Bonsai : Common
    {
        /// <summary>
        /// Name of the Bonsai tree 
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
     
        /// <summary>
        /// Age of the Bonsai 
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Age { get; set; }
        
        /// <summary>
        /// Kind of tree species bonsai is 
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Species { get; set; }
    }
}
