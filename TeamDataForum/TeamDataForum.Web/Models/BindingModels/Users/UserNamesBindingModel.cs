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
    }
}