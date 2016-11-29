namespace TeamDataForum.Web.Models.BindingModels.Users
{
    using System.ComponentModel.DataAnnotations;
    using Resources;

    public class PasswordBindingModel
    {
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
        [Display(Name = "Password")]
        public string Password { get; set; }

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
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

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
        [Compare("NewPassword",
            ErrorMessageResourceName = nameof(ModelsRes.ErrorUserConfirmPasswordMatch),
            ErrorMessageResourceType = typeof(ModelsRes))]
        [Display(Name = "Confirm new password")]
        public string ConfirmPassword { get; set; }
    }
}