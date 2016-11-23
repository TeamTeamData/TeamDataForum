namespace TeamDataForum.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Bases;
    using DBModels;
    using Models.BindingModels.Forums;
    using Models.BindingModels.Users;
    using UnitOfWork.Contracts;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class ForumController : ForumBaseController
    {
        public ForumController(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        // GET: Forum
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Empty create for creating Forum
        /// </summary>
        /// <returns>View for creating forum</returns>
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ForumBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            User user = this.UnitOfWork
                .UserRepository
                .Select(u => u.UserName == this.User.Identity.Name)
                .FirstOrDefault();

            Forum newForum = new Forum()
            {
                Title = model.Title,
                Description = model.Description,
                Date = DateTime.Now,
                Creator = user
            };

            newForum.Moderators.Add(user);

            newForum = this.UnitOfWork
                .ForumRepository
                .Add(newForum);

            this.UnitOfWork
                .SaveChanges();

            // to do change
            return RedirectToAction("Home", "Home", null);
        }
    }
}