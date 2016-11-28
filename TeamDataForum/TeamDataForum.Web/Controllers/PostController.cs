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

    public class PostController : ForumBaseController
    {
        public PostController(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        // GET: Post
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// This will be redirection to Thread controller
        /// and will show post with id
        /// </summary>
        /// <param name="id">post id</param>
        /// <returns>redirect to thread controller</returns>
        public ActionResult View(int id)
        {
            // to do
            return RedirectToAction("Home", "Thread", new { });
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
                return RedirectToAction("BadRequest", "Error");
            }

            ThreadPostBindingModel post = new ThreadPostBindingModel()
            {
                Thread = new IdentifiableThreadBindingModel()
                {
                    Id = thread.ThreadId,
                    Title = thread.Title
                }
            };

            return View(post);
        }

        /// <summary>
        /// Creates new post for specific thread
        /// </summary>
        /// <param name="id">Thread id</param>
        /// <param name="post">ThreadPostBindingModel</param>
        /// <returns>redirects</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, ThreadPostBindingModel post)
        {
            Thread thread = this.GetThread(id);

            if (thread == default(Thread) || thread.IsDeleted || id != post.Thread.Id)
            {
                return RedirectToAction("BadRequest", "Error");
            }

            if (!ModelState.IsValid)
            {
                post.Thread = new IdentifiableThreadBindingModel()
                {
                    Id = thread.ThreadId,
                    Title = thread.Title
                };

                return View(post);
            }

            User user = this.GetUser();

            Post newPost = new Post()
            {
                Creator = user,
                PostDate = DateTime.Now,
                Text = new PostText()
                {
                    Text = post.Text
                },
                Thread = thread
            };

            newPost = this.UnitOfWork
                .PostRepository
                .Add(newPost);

            this.UnitOfWork.SaveChanges();

            // to do
            return RedirectToAction("Home", "Home");
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
                return RedirectToAction("BadRequest", "Error");
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

            return View(postToEdit);
        }

        /// <summary>
        /// Edits post
        /// </summary>
        /// <param name="id">Post id</param>
        /// <param name="post">EditPostBindingModel</param>
        /// <returns>redirects</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditPostBindingModel post)
        {
            Post editPost = this.GetPost(id);

            if (editPost == default(Post) || editPost.IsDeleted || id != post.Id)
            {
                return RedirectToAction("BadRequest", "Error");
            }

            if (!ModelState.IsValid)
            {
                return this.View(post);
            }

            User user = this.UnitOfWork
                .UserRepository
                .Select(u => u.UserName == this.HttpContext.User.Identity.Name)
                .FirstOrDefault();

            editPost.Text.Text = post.Text;
            editPost.Changer = user;
            editPost.ChangeDate = DateTime.Now;

            this.UnitOfWork
                .PostRepository
                .Update(editPost);

            this.UnitOfWork.SaveChanges();

            // to do
            return RedirectToAction("Home", "Home");
        }

        /// <summary>
        /// Empty delete post
        /// </summary>
        /// <param name="id">Post id</param>
        /// <returns>View</returns>
        public ActionResult Delete(int id)
        {
            Post post = this.GetPost(id);

            if (post == default(Post) || post.IsDeleted)
            {
                return RedirectToAction("BadRequest", "Error");
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

            return View(postToDelete);
        }

        /// <summary>
        /// Delete for post
        /// </summary>
        /// <param name="id">Id of post</param>
        /// <param name="post">Post to be deleted</param>
        /// <returns>Redirects</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, PostDeleteBindingModel post)
        {
            Post postToDelete = this.GetPost(id);

            if (postToDelete == default(Post) || postToDelete.IsDeleted || id != post.Id)
            {
                return RedirectToAction("BadRequest", "Error");
            }

            if (!this.ModelState.IsValid)
            {
                return View(post);
            }

            postToDelete.IsDeleted = true;

            this.UnitOfWork
                .PostRepository
                .Update(postToDelete);

            this.UnitOfWork.SaveChanges();

            // to do
            return RedirectToAction("Home", "Home");
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