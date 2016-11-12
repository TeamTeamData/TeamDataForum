namespace TeamDataForum.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Bases;
    using Models.ViewModels.Posts;
    using Models.ViewModels.Subforums;
    using UnitOfWork.Contracts;

    public class HomeController : ForumBaseController
    {
        public HomeController(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        // GET: Home
        /// <summary>
        /// Display all subforums in forum
        /// </summary>
        /// <returns>ForumViewModel</returns>
        public ActionResult Home()
        {
            /// complex query
            /// only one query
            var subforums = this.UnitOfWork
                .SubforumRepository
                .Query
                .Where(s => !s.IsDeleted)
                .Select(s => new SubforumViewModel()
                {
                    Id = s.ForumId,
                    Title = s.Title,
                    Description = s.Description,
                    Date = s.Date,
                    Moderators = s.Moderators.Select(u => u.UserName),
                    Threads = s.Threads.Count,
                    Posts = s.Threads.SelectMany(t => t.Posts).Count(),
                    LatestPost = s.Threads
                    .Select(t => t.Posts.OrderByDescending(p => p.PostId).FirstOrDefault()).Select(p => new LatestPostViewModel
                    {
                        Id = p.PostId,
                        ThreadId = p.Thread.ThreadId,
                        Title = p.Thread.Title,
                        Author = p.Creator.UserName,
                        Date = p.PostDate
                    })
                    .FirstOrDefault()
                })
                .OrderBy(s => s.Id)
                .ToList();

            var forum = new ForumViewModel()
            {
                Subforums = subforums
            };

            return View(forum);
        }
    }
}