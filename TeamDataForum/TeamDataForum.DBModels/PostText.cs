namespace TeamDataForum.DBModels
{
    using System.ComponentModel.DataAnnotations;
    using Resources;

    /// <summary>
    /// PostText model for Entity framework
    /// </summary>
    public class PostText
    {
        /// <summary>
        /// Primary key
        /// </summary>
        [Key]
        public int PostTextId { get; set; }

        /// <summary>
        /// Post text
        /// Required
        /// </summary>
        [Required(AllowEmptyStrings = false,
            ErrorMessageResourceName = nameof(ModelsRes.ErrorPostTextTextRequired),
            ErrorMessageResourceType = typeof(ModelsRes))]
        [MinLength(NumericValues.PostTextMinValue,
            ErrorMessageResourceName = nameof(ModelsRes.ErrorPostTextMinValue),
            ErrorMessageResourceType = typeof(ModelsRes))]
        [MaxLength(NumericValues.PostTextMaxValue, 
            ErrorMessageResourceName = nameof(ModelsRes.ErrorPostTextMaxValue),
            ErrorMessageResourceType = typeof(ModelsRes))]
        public string Text { get; set; }

        /// <summary>
        /// Reference to Post model
        /// </summary>
        public virtual Post Post { get; set; }
    }
}
