namespace TeamDataForum.Web.Models.BindingModels.Users
{
    using System.ComponentModel.DataAnnotations;
    using Resources;

    public class UserRegistrationBindingModel
    {
        [Required(AllowEmptyStrings = false, 
            ErrorMessageResourceName = nameof(ModelsRes.ErrorUserFirstnameRequired),
            ErrorMessageResourceType = typeof(ModelsRes))]
        [MaxLength(NumericValues.NameMaxLength, 
            ErrorMessageResourceName = nameof(ModelsRes.ErrorUserNameMaxLength),
            ErrorMessageResourceType = typeof(ModelsRes))]
        [Display(Name = "First name")]
        public string Firstname { get; set; }

        [Required(AllowEmptyStrings = false, 
            ErrorMessageResourceName = nameof(ModelsRes.ErrorUserLastnameRequired),
            ErrorMessageResourceType = typeof(ModelsRes))]
        [MaxLength(NumericValues.NameMaxLength, 
            ErrorMessageResourceName = nameof(ModelsRes.ErrorUserNameMaxLength),
            ErrorMessageResourceType = typeof(ModelsRes))]
        [Display(Name = "Last name")]
        public string Lastname { get; set; }

        [Required(AllowEmptyStrings = false, 
            ErrorMessageResourceName = nameof(ModelsRes.ErrorUserUsernameRequired),
            ErrorMessageResourceType = typeof(ModelsRes))]
        [MaxLength(NumericValues.UsernameMaxLength, 
            ErrorMessageResourceName = nameof(ModelsRes.ErrorUserUsernameMaxLength),
            ErrorMessageResourceType = typeof(ModelsRes))]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required(AllowEmptyStrings = false, 
            ErrorMessageResourceName = nameof(ModelsRes.ErrorUserEmailRequired),
            ErrorMessageResourceType = typeof(ModelsRes))]
        [DataType(DataType.EmailAddress)]
        [MinLength(NumericValues.EmailMinLength,
            ErrorMessageResourceName = nameof(ModelsRes.ErrorUserEmailMinLength),
            ErrorMessageResourceType = typeof(ModelsRes))]
        [MaxLength(NumericValues.EmailMaxLength, 
            ErrorMessageResourceName = nameof(ModelsRes.ErrorUserEmailMaxLength),
            ErrorMessageResourceType = typeof(ModelsRes))]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, 
            ErrorMessageResourceName = nameof(ModelsRes.ErrorUserPasswordRequired),
            ErrorMessageResourceType = typeof(ModelsRes))]
        [DataType(DataType.Password)]
        [MinLength(NumericValues.PasswordMinLength,
            ErrorMessageResourceName = nameof(ModelsRes.ErrorUserPasswordMinLength),
            ErrorMessageResourceType = typeof(ModelsRes))]
        [MaxLength(NumericValues.PasswordMaxLength,
            ErrorMessageResourceName = nameof(ModelsRes.ErrorUserPasswordMaxLength),
            ErrorMessageResourceType = typeof(ModelsRes))]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false,
            ErrorMessageResourceName = nameof(ModelsRes.ErrorUserConfirmPasswordRequired),
            ErrorMessageResourceType = typeof(ModelsRes))]
        [DataType(DataType.Password)]
        [MinLength(NumericValues.PasswordMinLength, 
            ErrorMessageResourceName = nameof(ModelsRes.ErrorUserConfirmPasswordMinLength),
            ErrorMessageResourceType = typeof(ModelsRes))]
        [MaxLength(NumericValues.PasswordMaxLength,
            ErrorMessageResourceName = nameof(ModelsRes.ErrorUserConfrimPasswordMaxLength),
            ErrorMessageResourceType = typeof(ModelsRes))]
        [Compare("Password", 
            ErrorMessageResourceName = nameof(ModelsRes.ErrorUserConfirmPasswordMatch),
            ErrorMessageResourceType = typeof(ModelsRes))]
        public string ConfirmPassword { get; set; }
    }
}