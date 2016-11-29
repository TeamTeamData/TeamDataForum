namespace TeamDataForum.Web.Models.BindingModels.Users
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public class ImageUserBindingView
    {
        [Required]
        [Display(Name = "User Image")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase Image { get; set; }
    }
}