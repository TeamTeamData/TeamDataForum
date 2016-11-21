namespace TeamDataForum.Web.Models.BindingModels.Users
{
    using System.ComponentModel.DataAnnotations;
    using Resources;

    public class UserLogBindingModel
    {
        [Required(AllowEmptyStrings = false,
            ErrorMessageResourceName = nameof(ModelsRes.ErrorUserUsernameRequired),
            ErrorMessageResourceType = typeof(ModelsRes))]
        public string Username { get; set; }

        [Required(AllowEmptyStrings = false,
            ErrorMessageResourceName = nameof(ModelsRes.ErrorUserPasswordRequired),
            ErrorMessageResourceType = typeof(ModelsRes))]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}