namespace TeamDataForum.Web.Models.BindingModels.Threads
{
    using System.ComponentModel.DataAnnotations;
    using Resources;

    public class IdentifiableThreadBindingModel
    {
        [Required(ErrorMessageResourceName = nameof(ModelsRes.ErrorNoThread),
            ErrorMessageResourceType = typeof(ModelsRes))]
        public int Id { get; set; }

        public string Title { get; set; }
    }
}