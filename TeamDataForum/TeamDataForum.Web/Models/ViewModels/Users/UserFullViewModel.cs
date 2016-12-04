namespace TeamDataForum.Web.Models.ViewModels.Users
{
    using System.Collections.Generic;
    using Posts;

    public class UserFullViewModel
    {
        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        public string Image { get; set; }

        public string Town { get; set; }

        public string Country { get; set; }

        public int PostsCount { get; set; }

        public IEnumerable<UserPostViewModel> Posts { get; set; }
    }
}