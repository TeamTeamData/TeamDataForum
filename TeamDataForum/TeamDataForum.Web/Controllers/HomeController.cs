namespace TeamDataForum.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Bases;
    using Models.ViewModels.Forums;
    using Models.ViewModels.Posts;
    using Models.ViewModels.Threads;
    using Models.ViewModels.Users;
    using Pagination.Contracts;
    using UnitOfWork.Contracts;

    [Authorize]
    public class HomeController : ForumPageBaseController
    {
        private const int ThreadsToTake = 50;

        public HomeController(IUnitOfWork unitOfWork, IPaginationFactory paginationFactory) 
            : base(unitOfWork, paginationFactory)
        {
        }

        // GET: Home
        /// <summary>
        /// Display all subforums in forum
        /// </summary>
        /// <returns>ForumViewModel</returns>
        [AllowAnonymous]
        public ActionResult Home()
        {
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
                    Threads = f.Threads.Count(t => !t.IsDeleted),
                    Posts = f.Threads.Where(p => !p.IsDeleted).SelectMany(t => t.Posts).Count(p => !p.IsDeleted),
                    LatestPost = f.Threads
                    .Where(t => !t.IsDeleted)
                    .Select(t => t.Posts.Where(p => !p.IsDeleted).OrderByDescending(p => p.PostId).FirstOrDefault()).Select(p => new ForumPostViewModel
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

            // model
            var forum = new ForumViewModel()
            {
                Subforums = forums
            };

            // get data for current user
            forum.User = this.GetCurrentUser;

            // view
            return this.View(forum);
        }

        /// <summary>
        /// Shows information for specefic forums
        /// </summary>
        /// <param name="id">Forum Id</param>
        /// <returns>View</returns>
        [AllowAnonymous]
        public ActionResult View(int id, int? page)
        {
            int forumId = id;

            // query count
            var threadsCount = this.UnitOfWork
                .ThreadRepository
                .Count(t => t.Forum.ForumId == id);

            // pagination
            var pagination = this.PaginationFactory.CreatePagination(page, ThreadsToTake, threadsCount);

            var skipTake = pagination.ElementsToSkipAndTake();

            if (skipTake.Take > 0)
            {
                // query for forum with with more than one thread
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
                            Replies = t.Posts.Count(p => !p.IsDeleted),
                            TimesSeen = t.TimesSeen,
                            LastPost = t.Posts.Where(p => !p.IsDeleted).Select(p => new ThreadPostViewModel
                            {
                                Id = p.PostId,
                                ThreadId = p.Thread.ThreadId,
                                Text = p.Text.Text,
                                Author = new UserViewModel() { Id = p.Creator.Id, Username = p.Creator.UserName },
                                Date = p.PostDate
                            })
                            .OrderByDescending(p => p.Id)
                            .FirstOrDefault()
                        })
                        .OrderBy(tvm => tvm.Id)
                        .Skip(skipTake.Skip)
                        .Take(skipTake.Take)
                    })
                    .FirstOrDefault();

                // model pagination
                forum.Pages = pagination.GetPages("View", "Home");

                forum.User = this.GetCurrentUser;

                // view
                return this.View(forum);
            }

            // query for forum with no threads
            var forumNoThreads = this.UnitOfWork
                .ForumRepository
                .Query
                .Where(f => f.ForumId == forumId)
                .Select(f => new ForumFullViewModel()
                {
                    Id = f.ForumId,
                    Title = f.Title,
                    Description = f.Description,
                    Date = f.Date,
                    Moderators = f.Moderators.Select(u => new UserViewModel() { Id = u.Id, Username = u.UserName })
                })
                .FirstOrDefault();

            forumNoThreads.Threads = new List<ThreadViewModel>();

            // model pagination for forum with no threads
            forumNoThreads.Pages = pagination.GetPages("View", "Home");

            forumNoThreads.User = this.GetCurrentUser;

            // view
            return this.View(forumNoThreads);
        }
    }
}