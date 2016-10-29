namespace TeamDataForum.DBModels
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Town model for Entity framework
    /// </summary>
    public class Town
    {
        private const int TownNameMaxLength = 100;

        private const string TownNameError = "Town name is required.";
        private const string TownNameMaxLengthError = "Town name cannot be more than 100 symbols.";
        
        /// <summary>
        /// Primary key
        /// </summary>
        [Key]
        public int TownId { get; set; }

        /// <summary>
        /// Town name - required
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = TownNameError)]
        [MaxLength(TownNameMaxLength, ErrorMessage = TownNameMaxLengthError)]
        public string Name { get; set; }

        /// <summary>
        /// Reference to Country model
        /// </summary>
        public virtual Country Country { get; set; }
    }
}
