namespace TeamDataForum.Web.Models.ViewModels.Forums
{
    using System;
    using System.Collections.Generic;
    using Pagination.PaginationModels;
    using Threads;
    using Users;

    public class ForumFullViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public IEnumerable<UserViewModel> Moderators { get; set; }

        public IEnumerable<ThreadViewModel> Threads { get; set; }

        public IEnumerable<Paginator> Pages { get; set; }

        public CurrentUser User { get; set; }
    }
}