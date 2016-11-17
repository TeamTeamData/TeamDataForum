namespace TeamDataForum.Web.Models.BindingModels.Forums
{
    using System.ComponentModel.DataAnnotations;

    public class ForumBindingModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Forum title is required.")]
        [MaxLength(300, ErrorMessage = "Forum title cannot be more than 300 symbols.")]
        [Display(Name = "Forum title")]
        [DataType(DataType.MultilineText)]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Forum description is required.")]
        [MaxLength(300, ErrorMessage = "Forum description cannot be more than 300 symbols.")]
        [Display(Name = "Forum description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}