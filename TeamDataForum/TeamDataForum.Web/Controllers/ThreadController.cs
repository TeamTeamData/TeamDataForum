namespace TeamDataForum.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Bases;
    using Models.ViewModels.Posts;
    using Models.ViewModels.Threads;
    using Models.ViewModels.Users;
    using UnitOfWork.Contracts;

    /// <summary>
    /// Controller for threads 
    /// </summary>
    public class ThreadController : ForumBaseController
    {
        private const int PostsToTake = 10;

        public ThreadController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
        /// <summary>
        /// Selects all threads in specific subforum
        /// </summary>
        /// <param name="id">Subforum Id</param>
        /// <returns>View of ThreadsViewModel</returns>
        public ActionResult Home(int id, int? page)
        {
            int threadId = id;
            int currentPage = (page ?? 1) - 1;

            if (currentPage < 0)
            {
                currentPage = 0;
            }

            var postsCount = this.UnitOfWork
                .PostRepository
                .Count(p => p.Thread.ThreadId == id);

            int postsToSkip = currentPage * PostsToTake;

            int postsToTake = postsToSkip + PostsToTake > postsCount ?
                postsCount - postsToSkip :
                PostsToTake;

            var thread = this.UnitOfWork
                .ThreadRepository
                .Query
                .Where(t => t.ThreadId == id)
                .Select(t => new ThreadFullViewModel()
                {
                    Id = t.ThreadId,
                    Title = t.Title,
                    Creator = new UserViewModel() { Id = t.Creator.Id, Username = t.Creator.UserName },
                    CreationDate = t.Date,
                    Posts = t.Posts.Where(p => !p.IsDeleted).Select(p => new PostFullViewModel()
                    {
                        Id = p.PostId,
                        Date = p.PostDate,
                        Text = p.Text.Text,
                        Author = new UserViewModel() { Id = p.Creator.Id, Username = p.Creator.UserName },
                        ChangeDate = p.ChangeDate,
                        Changer = p.Changer.UserName
                    })
                    .OrderBy(p => p.Id)
                    .Skip(postsToSkip)
                    .Take(postsToTake)
                })
                .FirstOrDefault();

            thread.Pages = (postsCount / PostsToTake) + 1;

            int counter = postsToSkip + 1;

            foreach (var post in thread.Posts)
            {
                post.Number = counter;
                counter++;
            }

            return this.View(thread);
        }
    }
}