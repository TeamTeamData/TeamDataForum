namespace TeamDataForum.Web.Models.BindingModels.Users
{
    using System.ComponentModel.DataAnnotations;
    using Resources;

    public class UserNamesBindingModel
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
            ErrorMessageResourceName = nameof(ModelsRes.ErrorUserEmailRequired),
            ErrorMessageResourceType = typeof(ModelsRes))]
        [DataType(DataType.EmailAddress)]
        [MinLength(NumericValues.EmailMinLength,
            ErrorMessageResourceName = nameof(ModelsRes.ErrorUserEmailMinLength),
            ErrorMessageResourceType = typeof(ModelsRes))]
        [MaxLength(NumericValues.EmailMaxLength,
            ErrorMessageResourceName = nameof(ModelsRes.ErrorUserEmailMaxLength),
            ErrorMessageResourceType = typeof(ModelsRes))]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}