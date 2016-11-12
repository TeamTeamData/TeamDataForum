namespace TeamDataForum.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Bases;
    using Models.ViewModels.Posts;
    using Models.ViewModels.Threads;
    using UnitOfWork.Contracts;

    /// <summary>
    /// Controller for threads 
    /// </summary>
    public class ThreadController : ForumBaseController
    {
        public ThreadController(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        // GET: Thread
        // to do pagination
        /// <summary>
        /// Selects all threads in specific subforum
        /// </summary>
        /// <param name="id">Subforum Id</param>
        /// <returns>View of ThreadsViewModel</returns>
        public ActionResult Home(int id)
        {
            int threadId = id;

            var threads = this.UnitOfWork
                .ThreadRepository
                .Query
                .Where(t => t.Subforum.SubforumId == id && !t.IsDeleted)
                .Select(t => new ThreadViewModel()
                {
                    Id = t.ThreadId,
                    Title = t.Title,
                    TimesSeen = t.TimesSeen,
                    IsLocked = t.IsLocked,
                    Replies = t.Posts.Count,
                    LastPost = t.Posts.OrderByDescending(p => p.PostId).Select(p => new LatestPostViewModel()
                    {
                        Id = p.PostId,
                        ThreadId = p.Thread.ThreadId,
                        Title = p.Thread.Title,
                        Author = p.Creator.UserName,
                        Date = p.PostDate
                    })
                    .FirstOrDefault()
                })
                .OrderBy(t => t.Id)
                .ToList();

            ThreadsViewModel threadsViewModel = new ThreadsViewModel();
            threadsViewModel.Threads = threads;

            return View(threadsViewModel);
        }

        /// <summary>
        /// Controller for specific thread
        /// </summary>
        /// <param name="id">Thread Id</param>
        /// <returns>Returns view of postsViewModel</returns>
        public ActionResult View(int id)
        {
            int threadId = id;

            var posts = this.UnitOfWork
                .PostRepository
                .Select(p => p.Thread.ThreadId == threadId,
                q => q.OrderBy(p => p.PostId),
                new string[] { "Post.Text", "Post.Creator", "Post.Thread" });

            // to do automapper
            // convert posts

            return View();
        }
    }
}