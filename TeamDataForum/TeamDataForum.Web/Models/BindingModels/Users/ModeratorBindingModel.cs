namespace TeamDataForum.Web.Models.BindingModels.Users
{
    using System.ComponentModel.DataAnnotations;
    using Resources;

    public class ModeratorBindingModel
    {
        public string Id { get; set; }

        public int Number { get; set; }

        [Required(AllowEmptyStrings = false,
            ErrorMessageResourceName = nameof(ModelsRes.ErrorUserUsernameRequired),
            ErrorMessageResourceType = typeof(ModelsRes))]
        public string Username { get; set; }
    }
}