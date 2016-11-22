namespace TeamDataForum.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Bases;
    using Models.ViewModels.Posts;
    using Models.ViewModels.Threads;
    using Models.ViewModels.Users;
    using Pagination.Contracts;
    using UnitOfWork.Contracts;

    /// <summary>
    /// Controller for threads 
    /// </summary>
    public class ThreadController : ForumPageBaseController
    {
        private const int PostsToTake = 10;

        public ThreadController(IUnitOfWork unitOfWork, IPaginationFactory paginationFactory) 
            : base(unitOfWork, paginationFactory)
        {
        }

        /// <summary>
        /// Selects all threads in specific subforum
        /// </summary>
        /// <param name="id">Subforum Id</param>
        /// <returns>View of ThreadsViewModel</returns>
        public ActionResult Home(int id, int? page)
        {
            var postsCount = this.UnitOfWork
                .PostRepository
                .Count(p => p.Thread.ThreadId == id);

            var pagination = this.PaginationFactory.CreatePagination(page, PostsToTake, postsCount);

            var skipTake = pagination.ElementsToSkipAndTake();

            var thread = this.UnitOfWork
                .ThreadRepository
                .Query
                .Where(t => t.ThreadId == id)
                .Select(t => new ThreadFullViewModel()
                {
                    Id = t.ThreadId,
                    Title = t.Title,
                    Creator = new UserViewModel()
                    {
                        Id = t.Creator.Id,
                        Username = t.Creator.UserName
                    },
                    CreationDate = t.Date,
                    Posts = t.Posts.Where(p => !p.IsDeleted).Select(p => new PostFullViewModel()
                    {
                        Id = p.PostId,
                        Date = p.PostDate,
                        Text = p.Text.Text,
                        Author = new PostUserViewModel()
                        {
                            Id = p.Creator.Id,
                            Username = p.Creator.UserName,
                            Image = p.Creator.Image
                        },
                        ChangeDate = p.ChangeDate,
                        Changer = p.Changer.UserName
                    })
                    .OrderBy(p => p.Id)
                    .Skip(skipTake.Skip)
                    .Take(skipTake.Take)
                })
                .FirstOrDefault();

            int postNumber = skipTake.Skip + 1;

            foreach (var post in thread.Posts)
            {
                post.Number = postNumber;

                postNumber++;
            }

            thread.Pages = pagination.GetPages("Home", "Thread");

            return this.View(thread);
        }
    }
}