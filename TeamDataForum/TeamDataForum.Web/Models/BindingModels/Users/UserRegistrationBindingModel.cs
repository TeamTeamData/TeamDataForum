namespace TeamDataForum.Web.Models.BindingModels.Users
{
    using System.ComponentModel.DataAnnotations;

    public class UserRegistrationBindingModel
    {
        private const int MaxNameLength = 50;
        private const int TownCountryNameMaxLength = 100;

        [Required(AllowEmptyStrings = false, ErrorMessage = "First name is required.")]
        [MaxLength(MaxNameLength, ErrorMessage = "First name cannot be more than 50 symbols.")]
        [Display(Name = "First name")]
        public string Firstname { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Last name is required.")]
        [MaxLength(MaxNameLength, ErrorMessage = "Last name cannot be more than 50 symbols.")]
        [Display(Name = "Last name")]
        public string Lastname { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password cannot be less than 6 symbols.")]
        [MaxLength(50, ErrorMessage = "Password cannot be more than 50 sumbols.")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Confirm password cannot be less than 6 symbols.")]
        [MaxLength(50, ErrorMessage = "Confirm password cannot be more than 50 sumbols.")]
        public string ConfirmPassword { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Town name is required.")]
        [MaxLength(TownCountryNameMaxLength, ErrorMessage = "Town name cannot be more than 100 symbols.")]
        [Display(Name = "Town")]
        public string Town { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Country name is required.")]
        [MaxLength(TownCountryNameMaxLength, ErrorMessage = "Country name cannot be more than 100 symbols.")]
        [Display(Name = "Country")]
        public string Country { get; set; }
    }
}