namespace TeamDataForum.Web.Models.BindingModels.Forums
{
    using System.ComponentModel.DataAnnotations;

    public class DeleteForumBindingModel
    {
        [Required]
        public int Id { get; set; }

        [Display(Name = "Forum title: ")]
        public string Title { get; set; }

        [Display(Name = "Forum description: ")]
        public string Description { get; set; }
    }
}