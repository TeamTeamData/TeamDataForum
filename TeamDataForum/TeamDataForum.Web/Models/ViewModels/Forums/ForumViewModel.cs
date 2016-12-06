namespace TeamDataForum.Web.Models.ViewModels.Forums
{
    using System.Collections.Generic;
    using Models.ViewModels.Users;

    public class ForumViewModel
    {
        public IEnumerable<SubforumViewModel> Subforums { get; set; }

        public CurrentUser User { get; set; }
    }
}