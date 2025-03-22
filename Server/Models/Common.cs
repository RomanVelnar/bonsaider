using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    /// <summary>
    /// This is a common base model that will be included in all other models.
    /// </summary>
    public abstract class Common
    {
        /// <summary>
        /// Unique identifier for the entity.
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Timestamp when the entity was created.
        /// </summary>
        [Required]
        public DateTimeOffset CreatedDate { get; set; }

        /// <summary>
        /// Timestamp when the entity was last updated.
        /// </summary>
        [Required]
        public DateTimeOffset LastModifiedDate { get; set; }

        /// <summary>
        /// Constructor ensures default values.
        /// </summary>
        public Common()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTimeOffset.UtcNow;
            LastModifiedDate = DateTimeOffset.UtcNow;
        }
    }
}
