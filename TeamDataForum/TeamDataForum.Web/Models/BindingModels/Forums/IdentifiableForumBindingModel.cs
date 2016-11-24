namespace TeamDataForum.Web.Models.BindingModels.Forums
{
    using System.ComponentModel.DataAnnotations;
    using Resources;

    public class IdentifiableForumBindingModel
    {
        [Required(ErrorMessageResourceName = nameof(ModelsRes.ErrorNoForum),
            ErrorMessageResourceType = typeof(ModelsRes))]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}