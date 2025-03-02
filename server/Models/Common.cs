namespace Backend.Models
{
    /// <summary>
    /// This is a basic/common model that will be included in all other models
    /// </summary>
    public abstract class Common
    {
        /// <summary>
        /// Unique identifier for the entity
        /// </summary>
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Time stamp when the entity was created
        /// </summary>
        public DateTimeOffset CreatedDate
        {
            get; set;
        } = DateTimeOffset.UtcNow;

        /// <summary>
        /// Time stamp when the entity was last updated
        /// </summary>
        public DateTimeOffset LastModifiedDate { get; set; } = DateTimeOffset.UtcNow;
    }

}