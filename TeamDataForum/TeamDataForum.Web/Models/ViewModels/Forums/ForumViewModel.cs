namespace TeamDataForum.Web.Models.ViewModels.Forums
{
    using System.Collections.Generic;

    public class ForumViewModel
    {
        public IEnumerable<SubforumViewModel> Subforums { get; set; }
    }
}