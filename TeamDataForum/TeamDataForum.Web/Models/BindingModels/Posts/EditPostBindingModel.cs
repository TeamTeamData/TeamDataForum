namespace TeamDataForum.Web.Models.BindingModels.Posts
{
    using System.ComponentModel.DataAnnotations;

    public class EditPostBindingModel : ThreadPostBindingModel
    {
        [Required]
        public int Id { get; set; }
    }
}