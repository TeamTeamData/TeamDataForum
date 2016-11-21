namespace TeamDataForum.DBModels
{
    using System.ComponentModel.DataAnnotations;
    using Resources;

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
        [Required(AllowEmptyStrings = false,
            ErrorMessageResourceName = nameof(ModelsRes.ErrorTownNameRequired),
            ErrorMessageResourceType = typeof(ModelsRes))]
        [MaxLength(NumericValues.PlaceNameMaxLength,
            ErrorMessageResourceName = nameof(ModelsRes.ErrorTownNameMaxLength),
            ErrorMessageResourceType = typeof(ModelsRes))]
        public string Name { get; set; }

        /// <summary>
        /// Reference to Country model
        /// </summary>
        public virtual Country Country { get; set; }
    }
}
