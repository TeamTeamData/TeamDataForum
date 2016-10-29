namespace TeamDataForum.DBModels
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// User model for entity framework
    /// to do add inheritance to identity user
    /// </summary>
    public class User
    {
        /// <summary>
        /// First name - required
        /// </summary>
        [Required]
        [MaxLength(50, ErrorMessage = "First name is required.")]
        public string Firstname { get; set; }

        /// <summary>
        /// Last name - required
        /// </summary>
        [Required]
        [MaxLength(50, ErrorMessage = "Last name is required.")]
        public string Lastname { get; set; }

        /// <summary>
        /// Reference to Town model
        /// </summary>
        public virtual Town Town { get; set; }
    }
}
