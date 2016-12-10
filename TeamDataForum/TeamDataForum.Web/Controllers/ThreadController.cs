namespace TeamDataForum.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Bases;
    using DBModels;
    using Models.BindingModels.Forums;
    using Models.BindingModels.Threads;
    using Models.ViewModels.Posts;
    using Models.ViewModels.Threads;
    using Models.ViewModels.Users;
    using Pagination.Contracts;
    using Resources;
    using UnitOfWork.Contracts;

    /// <summary>
    /// Controller for threads 
    /// </summary>
    [Authorize]
    public class ThreadController : ForumPageBaseController
    {
        public ThreadController(IUnitOfWork unitOfWork, IPaginationFactory paginationFactory) 
            : base(unitOfWork, paginationFactory)
        {
        }

        /// <summary>
        /// Selects all threads in specific subforum
        /// </summary>
        /// <param name="id">Subforum Id</param>
        /// <returns>View of ThreadsViewModel</returns>
        [AllowAnonymous]
        public ActionResult Home(int id, int? page)
        {
            // query count
            var postsCount = this.UnitOfWork
                .PostRepository
                .Count(p => p.Thread.ThreadId == id);

            // pagination
            var pagination = this.PaginationFactory.CreatePagination(page, NumericValues.PostsToTake, postsCount);

            var skipTake = pagination.ElementsToSkipAndTake();

            // query
            var thread = this.UnitOfWork
                .ThreadRepository
                .Query
                .Where(t => t.ThreadId == id)
                .Select(t => new ThreadFullViewModel()
                {
                    Id = t.ThreadId,
                    ForumId = t.Forum.ForumId,
                    Title = t.Title,
                    Creator = new UserViewModel()
                    {
                        Id = t.Creator.Id,
                        Username = t.Creator.UserName
                    },
                    CreationDate = t.Date,
                    Moderators = t.Forum.Moderators.Select(u => new UserViewModel
                    {
                        Id = u.Id,
                        Username = u.UserName
                    }),
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

            // model pagination
            thread.Pages = pagination.GetPages("Home", "Thread");

            // set post number in thread
            int pageNumber = skipTake.Skip + 1;

            foreach (var post in thread.Posts)
            {
                post.Number = pageNumber++;
            }

            thread.User = this.GetCurrentUser;

            // view
            return this.View(thread);
        }

        /// <summary>
        /// Create empty model for thread creation
        /// </summary>
        /// <param name="id">forum id</param>
        /// <returns>view for creation</returns>
        public ActionResult Create(int id)
        {
            Forum forum = this.GetForum(id);

            if (forum == default(Forum) || forum.IsDeleted)
            {
                return this.RedirectToAction("BadRequest", "Error");
            }

            ThreadBindingModel thread = new ThreadBindingModel()
            {
                Forum = new IdentifiableForumBindingModel()
                {
                    Id = forum.ForumId,
                    Title = forum.Title,
                    Description = forum.Description
                }
            };

            return this.View(thread);
        }

        /// <summary>
        /// Create new thread
        /// </summary>
        /// <param name="model">thread to create</param>
        /// <param name="id">Parent forum id</param>
        /// <returns>redirect to home home</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ThreadBindingModel model, int id)
        {
            Forum forum = this.GetForum(id);

            if (forum == default(Forum) || forum.IsDeleted)
            {
                return this.RedirectToAction("BadRequest", "Error");
            }

            if (!this.ModelState.IsValid)
            {
                model.Forum = new IdentifiableForumBindingModel()
                {
                    Id = forum.ForumId,
                    Title = forum.Title,
                    Description = forum.Description
                };

                return this.View(model);
            }

            User user = this.GetUser();

            Thread newThread = new Thread()
            {
                Creator = user,
                Date = DateTime.Now,
                Forum = forum,
                Title = model.Title,
            };

            newThread.Posts.Add(new Post()
            {
                Creator = user,
                PostDate = DateTime.Now,
                Text = new PostText()
                {
                    Text = model.Text
                }
            });

            newThread = this.UnitOfWork
                .ThreadRepository
                .Add(newThread);

            this.UnitOfWork.SaveChanges();

            return this.RedirectToAction("Home", "Thread", new { Id = newThread.ThreadId });
        }

        /// <summary>
        /// Empty edit for thread
        /// </summary>
        /// <param name="id">Thread id</param>
        /// <returns>View</returns>
        [Authorize(Roles = "Moderator, Administrator")]
        public ActionResult Edit(int id)
        {
            Thread thread = this.GetThread(id);

            if (thread == default(Thread) || thread.IsDeleted)
            {
                return this.RedirectToAction("BadRequest", "Error");
            }

            EditThreadBindingModel editThread = new EditThreadBindingModel()
            {
                Id = thread.ThreadId,
                Title = thread.Title
            };

            return this.View(editThread);
        }

        /// <summary>
        /// Edit controller
        /// </summary>
        /// <param name="id">Thread id</param>
        /// <param name="model">Thread data to be edited</param>
        /// <returns>Redirects</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Moderator, Administrator")]
        public ActionResult Edit(int id, EditThreadBindingModel model)
        {
            Thread editThread = this.GetThread(id);

            if (editThread == default(Thread) || editThread.IsDeleted || id != model.Id)
            {
                return this.RedirectToAction("BadRequest", "Error");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            editThread.Title = model.Title;

            editThread = this.UnitOfWork
                .ThreadRepository
                .Update(editThread);

            this.UnitOfWork.SaveChanges();

            return this.RedirectToAction("Home", "Thread", new { Id = editThread.ThreadId });
        }

        [Authorize(Roles = "Moderator, Administrator")]
        public ActionResult Delete(int id)
        {
            Thread thread = this.GetThread(id);

            if (thread == default(Thread) || thread.IsDeleted)
            {
                return this.RedirectToAction("BadRequest", "Error");
            }

            DeleteThreadBindingModel deleteThread = new DeleteThreadBindingModel()
            {
                Id = thread.ThreadId,
                Title = thread.Title
            };

            return this.View(deleteThread);
        }

        /// <summary>
        /// Action to delete thread
        /// </summary>
        /// <param name="id">Thread id</param>
        /// <param name="model">DeleteThreadBindingModel</param>
        /// <returns>Redirects</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Moderator, Administrator")]
        public ActionResult Delete(int id, DeleteThreadBindingModel model)
        {
            Thread deleteThread = this.GetThread(id);

            if (deleteThread == default(Thread) || deleteThread.IsDeleted || id != model.Id)
            {
                return this.RedirectToAction("BadRequest", "Error");
            }

            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("BadRequest", "Error");
            }

            deleteThread.IsDeleted = true;

            this.UnitOfWork
                .ThreadRepository
                .Update(deleteThread);

            this.UnitOfWork.SaveChanges();

            return this.RedirectToAction("Home", "Home");
        }

        private Forum GetForum(int id)
        {
            Forum forum = this.UnitOfWork
                .ForumRepository
                .Find(id);

            return forum;
        }

        private Thread GetThread(int id)
        {
            Thread thread = this.UnitOfWork
                .ThreadRepository
                .Find(id);

            return thread;
        }

        private User GetUser()
        {
            User user = this.UnitOfWork
                .UserRepository
                .Select(u => u.UserName == this.HttpContext.User.Identity.Name)
                .FirstOrDefault();

            return user;
        }
    }
}