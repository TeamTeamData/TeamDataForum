namespace TeamDataForum.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Bases;
    using Models.ViewModels.Subforums;
    using UnitOfWork.Contracts;

    public class HomeController : ForumBaseController
    {
        public HomeController(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        // GET: Home
        public ActionResult Home()
        {
            var subforums = this.UnitOfWork
                .SubforumRepository
                .Query
                .Where(s => !s.IsDeleted)
                .Select(s => new SubforumViewModel()
                {
                    Title = s.Title,
                    Description = s.Description,
                    Date = s.Date,
                    Moderators = s.Moderators.Select(u => u.UserName),
                    Threads = s.Threads.Count,
                    Posts = s.Threads.SelectMany(t => t.Posts).Count()
                })
                .ToList();

            return View(subforums);
        }
    }
}