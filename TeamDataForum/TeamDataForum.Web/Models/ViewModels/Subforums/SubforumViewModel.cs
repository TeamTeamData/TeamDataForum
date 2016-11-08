namespace TeamDataForum.Web.Models.ViewModels.Subforums
{
    using System;
    using System.Collections.Generic;

    public class SubforumViewModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public IEnumerable<string> Moderators { get; set; }

        public int Threads { get; set; }

        public int Posts { get; set; }
    }
}