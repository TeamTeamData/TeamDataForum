namespace TeamDataForum.Web.Models.ViewModels.Posts
{
    using System;
    using Users;

    public class ThreadPostViewModel
    {
        public int Id { get; set; }

        public int ThreadId { get; set; }

        public string Text { get; set; }

        public UserViewModel Author { get; set; }

        public DateTime Date { get; set; }
    }
}