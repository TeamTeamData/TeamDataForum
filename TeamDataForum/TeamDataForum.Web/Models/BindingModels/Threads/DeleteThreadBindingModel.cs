namespace TeamDataForum.Web.Models.BindingModels.Threads
{
    using System.ComponentModel.DataAnnotations;
    using Resources;

    public class DeleteThreadBindingModel
    {
        [Required]
        public int Id { get; set; }

        public string Title { get; set; }
    }
}