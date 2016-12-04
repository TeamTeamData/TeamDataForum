namespace TeamDataForum.Web.Models.ViewModels.Posts
{
    public class UserPostViewModel
    {
        public int PostId { get; set; }

        public int ThreadId { get; set; }

        public string Text { get; set; }

        public string Thread { get; set; }
    }
}