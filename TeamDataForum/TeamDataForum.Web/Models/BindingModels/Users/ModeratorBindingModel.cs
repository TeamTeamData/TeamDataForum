namespace TeamDataForum.Web.Models.BindingModels.Users
{
    using System.ComponentModel.DataAnnotations;
    using Resources;

    public class ModeratorBindingModel
    {
        [Required(AllowEmptyStrings = false,
            ErrorMessageResourceName = nameof(ModelsRes.ErrorModeratorIdRequired),
            ErrorMessageResourceType = typeof(ModelsRes))]
        public string Id { get; set; }

        public int Number { get; set; }

        public string Username { get; set; }
    }
}