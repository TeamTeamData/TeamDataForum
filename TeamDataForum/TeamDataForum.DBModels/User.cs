namespace TeamDataForum.DBModels
{
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        [Required]
        [MaxLength(50, ErrorMessage = "First name is required.")]
        public string Firstname { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Last name is required.")]
        public string Lastname { get; set; }

        public virtual Town Town { get; set; }
    }
}
