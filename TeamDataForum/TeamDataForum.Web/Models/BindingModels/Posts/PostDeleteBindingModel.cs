namespace TeamDataForum.Web.Models.BindingModels.Posts
{
    using System.ComponentModel.DataAnnotations;
    using Resources;
    using Threads;

    public class PostDeleteBindingModel
    {
        [Required]
        public int Id { get; set; }

        public IdentifiableThreadBindingModel Thread { get; set; }

        public string Text { get; set; }
    }
}