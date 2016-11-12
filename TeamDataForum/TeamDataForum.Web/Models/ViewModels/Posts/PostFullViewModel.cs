namespace TeamDataForum.Web.Models.ViewModels.Posts
{
    using System;

    public class PostFullViewModel
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string Author { get; set; }

        public DateTime Date { get; set; }
    }
}