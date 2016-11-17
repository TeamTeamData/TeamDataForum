namespace TeamDataForum.Web.Models.ViewModels.Threads
{
    using System;
    using System.Collections.Generic;
    using Posts;
    using Users;

    public class ThreadFullViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public UserViewModel Creator { get; set; }

        public DateTime CreationDate { get; set; }

        public int Pages { get; set; }

        public int Page { get; set; }

        public IEnumerable<PostFullViewModel> Posts { get; set; }
    }
}