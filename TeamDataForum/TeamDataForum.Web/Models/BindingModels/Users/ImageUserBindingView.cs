namespace TeamDataForum.Web.Models.BindingModels.Users
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public class ImageUserBindingView
    {
        [Required]
        [Display(Name = "User Image")]
        public HttpPostedFileBase Image { get; set; }
    }
}