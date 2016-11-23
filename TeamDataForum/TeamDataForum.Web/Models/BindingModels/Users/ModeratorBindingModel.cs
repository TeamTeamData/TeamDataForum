namespace TeamDataForum.Web.Models.BindingModels.Users
{
    using System.ComponentModel.DataAnnotations;
    using Resources;

    public class ModeratorBindingModel
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false,
            ErrorMessageResourceName = nameof(ModelsRes.ErrorUserUsernameRequired),
            ErrorMessageResourceType = typeof(ModelsRes))]
        public string Username { get; set; }
    }
}