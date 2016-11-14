namespace TeamDataForum.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Bases;
    using Models.ViewModels.Forums;
    using Models.ViewModels.Posts;
    using Models.ViewModels.Threads;
    using Models.ViewModels.Users;
    using UnitOfWork.Contracts;

    public class HomeController : ForumBaseController
    {
        private const int ThreadsToTake = 50;

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
            var forums = this.UnitOfWork
                .ForumRepository
                .Query
                .Where(f => !f.IsDeleted)
                .Select(f => new SubforumViewModel()
                {
                    Id = f.ForumId,
                    Title = f.Title,
                    Description = f.Description,
                    Date = f.Date,
                    Moderators = f.Moderators.Select(u => new UserViewModel() { Id = u.Id, Username = u.UserName }),
                    Threads = f.Threads.Count,
                    Posts = f.Threads.Where(p => !p.IsDeleted).SelectMany(t => t.Posts).Count(),
                    LatestPost = f.Threads
                    .Select(t => t.Posts.Where(p => !p.IsDeleted).OrderByDescending(p => p.PostId).FirstOrDefault()).Select(p => new PostViewModel
                    {
                        Id = p.PostId,
                        ThreadId = p.Thread.ThreadId,
                        Title = p.Thread.Title,
                        Author = new UserViewModel() { Id = p.Creator.Id, Username = p.Creator.UserName },
                        Date = p.PostDate
                    })
                    .FirstOrDefault()
                })
                .OrderBy(s => s.Id)
                .ToList();

            var forum = new ForumViewModel()
            {
                Subforums = forums
            };

            return View(forum);
        }

        /// <summary>
        /// Shows information for specefic forums
        /// </summary>
        /// <param name="id">Forum Id</param>
        /// <returns></returns>
        public ActionResult View(int id, int? page)
        {
            int forumId = id;
            int currentPage = (page ?? 1) - 1;

            if (currentPage < 0)
            {
                currentPage = 0;
            }

            var threadsCount = this.UnitOfWork
                .ThreadRepository
                .Count(t => t.Forum.ForumId == id);

            int threadsToSkip = currentPage * ThreadsToTake;

            int threadsToTake = threadsToSkip + ThreadsToTake > threadsCount ?
                threadsCount - threadsToSkip :
                ThreadsToTake;

            var forum = this.UnitOfWork
                .ForumRepository
                .Query
                .Where(f => f.ForumId == forumId)
                .Select(f => new ForumFullViewModel()
                {
                    Id = f.ForumId,
                    Title = f.Title,
                    Description = f.Description,
                    Date = f.Date,
                    Moderators = f.Moderators.Select(u => new UserViewModel() { Id = u.Id, Username = u.UserName }),
                    Threads = f.Threads.Where(t => !t.IsDeleted).Select(t => new ThreadViewModel()
                    {
                        Id = t.ThreadId,
                        Title = t.Title,
                        IsLocked = t.IsLocked,
                        Replies = t.Posts.Count,
                        TimesSeen = t.TimesSeen,
                        LastPost = t.Posts.Where(p => !p.IsDeleted).Select(p => new PostViewModel
                        {
                            Id = p.PostId,
                            ThreadId = p.Thread.ThreadId,
                            Title = p.Thread.Title,
                            Author = new UserViewModel() { Id = p.Creator.Id, Username = p.Creator.UserName },
                            Date = p.PostDate
                        })
                        .OrderByDescending(p => p.Id)
                        .FirstOrDefault()
                    })
                    .OrderBy(tvm => tvm.Id)
                    .Skip(threadsToSkip)
                    .Take(threadsToTake)
                })
                .FirstOrDefault();

            forum.Pages = (threadsCount / ThreadsToTake) + 1;

            return View(forum);
        }
    }
}