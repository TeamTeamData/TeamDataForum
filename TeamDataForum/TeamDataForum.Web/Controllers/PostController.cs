namespace TeamDataForum.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Bases;
    using DBModels;
    using Models.BindingModels.Posts;
    using Models.BindingModels.Threads;
    using UnitOfWork.Contracts;

    [Authorize]
    public class PostController : ForumBaseController
    {
        public PostController(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        // GET: Post
        [AllowAnonymous]
        public ActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// This will be redirection to Thread controller
        /// and will show post with id
        /// </summary>
        /// <param name="id">post id</param>
        /// <returns>redirect to thread controller</returns>
        [AllowAnonymous]
        public ActionResult View(int id)
        {
            // to do
            return this.RedirectToAction("Home", "Thread", new { });
        }

        /// <summary>
        /// Empty create for post
        /// </summary>
        /// <param name="id">Thread id</param>
        /// <returns>View of ThreadPostBindingModel</returns>
        public ActionResult Create(int id)
        {
            Thread thread = this.GetThread(id);

            if (thread == default(Thread) || thread.IsDeleted)
            {
                return this.RedirectToAction("BadRequest", "Error");
            }

            ThreadPostBindingModel post = new ThreadPostBindingModel()
            {
                Thread = new IdentifiableThreadBindingModel()
                {
                    Id = thread.ThreadId,
                    Title = thread.Title
                }
            };

            return this.View(post);
        }

        /// <summary>
        /// Creates new post for specific thread
        /// </summary>
        /// <param name="id">Thread id</param>
        /// <param name="model">ThreadPostBindingModel</param>
        /// <returns>redirects</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, ThreadPostBindingModel model)
        {
            Thread thread = this.GetThread(id);

            if (thread == default(Thread) || thread.IsDeleted || id != model.Thread.Id)
            {
                return this.RedirectToAction("BadRequest", "Error");
            }

            if (!this.ModelState.IsValid)
            {
                model.Thread = new IdentifiableThreadBindingModel()
                {
                    Id = thread.ThreadId,
                    Title = thread.Title
                };

                return this.View(model);
            }

            User user = this.GetUser();

            Post newPost = new Post()
            {
                Creator = user,
                PostDate = DateTime.Now,
                Text = new PostText()
                {
                    Text = model.Text
                },
                Thread = thread
            };

            newPost = this.UnitOfWork
                .PostRepository
                .Add(newPost);

            this.UnitOfWork.SaveChanges();

            // to do
            return this.RedirectToAction("Home", "Home");
        }

        /// <summary>
        /// Empty action for edit
        /// </summary>
        /// <param name="id">Id of post</param>
        /// <returns>View of EditPostBindingModel</returns>
        public ActionResult Edit(int id)
        {
            Post post = this.GetPost(id);

            if (post == default(Post) || post.IsDeleted)
            {
                return this.RedirectToAction("BadRequest", "Error");
            }

            EditPostBindingModel postToEdit = new EditPostBindingModel()
            {
                Id = post.PostId,
                Text = post.Text.Text,
                Thread = new IdentifiableThreadBindingModel()
                {
                    Id = post.Thread.ThreadId,
                    Title = post.Thread.Title
                }
            };

            return this.View(postToEdit);
        }

        /// <summary>
        /// Edits post
        /// </summary>
        /// <param name="id">Post id</param>
        /// <param name="model">EditPostBindingModel</param>
        /// <returns>redirects</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditPostBindingModel model)
        {
            Post editPost = this.GetPost(id);

            if (editPost == default(Post) || editPost.IsDeleted || id != model.Id)
            {
                return this.RedirectToAction("BadRequest", "Error");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            User user = this.UnitOfWork
                .UserRepository
                .Select(u => u.UserName == this.HttpContext.User.Identity.Name)
                .FirstOrDefault();

            editPost.Text.Text = model.Text;
            editPost.Changer = user;
            editPost.ChangeDate = DateTime.Now;

            this.UnitOfWork
                .PostRepository
                .Update(editPost);

            this.UnitOfWork.SaveChanges();

            // to do
            return this.RedirectToAction("Home", "Home");
        }

        /// <summary>
        /// Empty delete post
        /// </summary>
        /// <param name="id">Post id</param>
        /// <returns>View</returns>
        [Authorize(Roles = "Administrator, Moderator")]
        public ActionResult Delete(int id)
        {
            Post post = this.GetPost(id);

            if (post == default(Post) || post.IsDeleted)
            {
                return this.RedirectToAction("BadRequest", "Error");
            }

            PostDeleteBindingModel postToDelete = new PostDeleteBindingModel()
            {
                Id = post.PostId,
                Text = post.Text.Text,
                Thread = new IdentifiableThreadBindingModel()
                {
                    Id = post.Thread.ThreadId,
                    Title = post.Thread.Title
                }
            };

            return this.View(postToDelete);
        }

        /// <summary>
        /// Delete for post
        /// </summary>
        /// <param name="id">Id of post</param>
        /// <param name="model">Post to be deleted</param>
        /// <returns>Redirects</returns>
        [Authorize(Roles = "Administrator, Moderator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, PostDeleteBindingModel model)
        {
            Post postToDelete = this.GetPost(id);

            if (postToDelete == default(Post) || postToDelete.IsDeleted || id != model.Id)
            {
                return this.RedirectToAction("BadRequest", "Error");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            postToDelete.IsDeleted = true;

            this.UnitOfWork
                .PostRepository
                .Update(postToDelete);

            this.UnitOfWork.SaveChanges();

            // to do
            return this.RedirectToAction("Home", "Home");
        }

        private Thread GetThread(int id)
        {
            Thread thread = this.UnitOfWork
                .ThreadRepository
                .Find(id);

            return thread;
        }

        private Post GetPost(int id)
        {
            Post post = this.UnitOfWork
                .PostRepository
                .Find(id, new string[] { "Text", "Thread" });

            return post;
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