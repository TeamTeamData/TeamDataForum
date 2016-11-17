namespace TeamDataForum.DBModels
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// PostText model for Entity framework
    /// </summary>
    public partial class PostText
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
        [Required(AllowEmptyStrings = false, ErrorMessage = PostTextError)]
        public string Text { get; set; }

        /// <summary>
        /// Reference to Post model
        /// </summary>
        public virtual Post Post { get; set; }
    }
}
