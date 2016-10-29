namespace TeamDataForum.DBModels
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// User model for entity framework
    /// to do add inheritance to identity user
    /// </summary>
    public class User
    {
        private const int NameMaxLength = 50;

        private const string FirstnameError = "First name is required.";
        private const string LastnameError = "Last name is required.";
        private const string NameLengthError = "Name cannot be more than 50 symbols.";

        /// <summary>
        /// First name - required
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = FirstnameError)]
        [MaxLength(NameMaxLength, ErrorMessage = NameLengthError)]
        public string Firstname { get; set; }

        /// <summary>
        /// Last name - required
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = LastnameError)]
        [MaxLength(NameMaxLength, ErrorMessage = NameLengthError)]
        public string Lastname { get; set; }

        /// <summary>
        /// Reference to Town model
        /// </summary>
        public virtual Town Town { get; set; }
    }
}
