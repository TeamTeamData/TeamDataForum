namespace TeamDataForum.Web.Models.ViewModels.Threads
{
    using Posts;

    public class ThreadViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int TimesSeen { get; set; }

        public bool IsLocked { get; set; }

        public int Replies { get; set; }

        public ThreadPostViewModel LastPost { get; set; }
    }
}