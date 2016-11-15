namespace TeamDataForum.Web.Models.BindingModels.Users
{
    using System.ComponentModel.DataAnnotations;

    public class UserRegistrationBindingModel
    {
        private const int MaxNameLength = 50;
        private const int UsernameMaxLength = 256;

        [Required(AllowEmptyStrings = false, ErrorMessage = "First name is required.")]
        [MaxLength(MaxNameLength, ErrorMessage = "First name cannot be more than 50 symbols.")]
        [Display(Name = "First name")]
        public string Firstname { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Last name is required.")]
        [MaxLength(MaxNameLength, ErrorMessage = "Last name cannot be more than 50 symbols.")]
        [Display(Name = "Last name")]
        public string Lastname { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Username is required.")]
        [MaxLength(UsernameMaxLength, ErrorMessage = "Username cannot be more than 256 symbols.")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password cannot be less than 6 symbols.")]
        [MaxLength(50, ErrorMessage = "Password cannot be more than 50 sumbols.")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Confirm password cannot be less than 6 symbols.")]
        [MaxLength(50, ErrorMessage = "Confirm password cannot be more than 50 sumbols.")]
        [Compare("Password", ErrorMessage = "Confirm password does not match password.")]
        public string ConfirmPassword { get; set; }
    }
}