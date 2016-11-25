namespace TeamDataForum.Web.Models.BindingModels.Posts
{
    using System.ComponentModel.DataAnnotations;
    using Resources;
    using Threads;
    

    public class ThreadPostBindingModel
    {
        [Required(ErrorMessageResourceName = nameof(ModelsRes.ErrorNoThread),
            ErrorMessageResourceType = typeof(ModelsRes))]
        public IdentifiableThreadBindingModel Thread { get; set; }

        [Required(ErrorMessageResourceName = nameof(ModelsRes.ErrorThreadNoPost),
            ErrorMessageResourceType = typeof(ModelsRes))]
        public PostBindingModel Post { get; set; }
    }
}