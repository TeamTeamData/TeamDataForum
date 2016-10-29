namespace TeamDataForum.DBModels
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Town model for Entity framework
    /// </summary>
    public class Town
    {
        /// <summary>
        /// Primary key
        /// </summary>
        [Key]
        public int TownId { get; set; }

        /// <summary>
        /// Town name - required
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Reference to Country model
        /// </summary>
        public virtual Country Country { get; set; }
    }
}
