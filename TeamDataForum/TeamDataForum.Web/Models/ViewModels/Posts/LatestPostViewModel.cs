namespace TeamDataForum.Web.Models.ViewModels.Posts
{
    using System;

    public class LatestPostViewModel
    {
        public int Id { get; set; }

        public int ThreadId { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public DateTime Date { get; set; }
    }
}