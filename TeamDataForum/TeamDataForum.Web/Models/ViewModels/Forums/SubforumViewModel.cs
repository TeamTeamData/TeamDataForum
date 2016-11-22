namespace TeamDataForum.Web.Models.ViewModels.Forums
{
    using System;
    using System.Collections.Generic;
    using Posts;
    using Users;

    public class SubforumViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public IEnumerable<UserViewModel> Moderators { get; set; }

        public int Threads { get; set; }

        public int Posts { get; set; }

        public ForumPostViewModel LatestPost { get; set; }
    }
}