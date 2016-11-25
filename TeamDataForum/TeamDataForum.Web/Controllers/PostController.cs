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

        public ActionResult Create(int id)
        {
            Thread thread = this.GetThread(id);

            // to do
            if (thread == default(Thread))
            {
                return RedirectToAction("Home", "Home");
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, ThreadPostBindingModel post)
        {
            Thread thread = this.GetThread(id);

            // to do
            if (thread == default(Thread))
            {
                return RedirectToAction("Home", "Home");
            }

            if (!ModelState.IsValid)
            {
                return View(post);
            }

            User user = this.UnitOfWork
                .UserRepository
                .Select(u => u.UserName == this.HttpContext.User.Identity.Name)
                .FirstOrDefault();

            Post newPost = new Post()
            {
                Creator = user,
                PostDate = DateTime.Now,
                Text = new PostText()
                {
                    Text = post.Post.Text
                },
                Thread = thread
            };

            newPost = this.UnitOfWork
                .PostRepository
                .Add(newPost);

            this.UnitOfWork.SaveChanges();

            // to do
            return RedirectToAction("View", "Post", new { id = newPost.PostId });
        }

        private Thread GetThread(int id)
        {
            Thread thread = this.UnitOfWork
                .ThreadRepository
                .Find(id);

            return thread;
        }
    }
}