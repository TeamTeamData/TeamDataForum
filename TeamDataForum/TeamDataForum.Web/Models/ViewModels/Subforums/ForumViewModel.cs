namespace TeamDataForum.Web.Models.ViewModels.Subforums
{
    using System.Collections.Generic;

    public class ForumViewModel
    {
        public IEnumerable<SubforumViewModel> Subforums { get; set; }
    }
}