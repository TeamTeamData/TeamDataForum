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

    public class ForumController : ForumBaseController
    {
        private IEnumerable<ModeratorBindingModel> moderators;

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
            var role = this.RoleManager
                .Roles
                .FirstOrDefault(r => r.Name == "Moderator");

            var moderators = this.UnitOfWork
                .UserRepository
                .Select(u => u.Roles.Any(
                    r => r.RoleId == role.Id ));

            ForumBindingModel model = new ForumBindingModel();
            model.Moderators = this.GetModerators();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ForumBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Moderators = this.GetModerators();

                return View(model);
            }

            // to do
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

        private IEnumerable<ModeratorBindingModel> GetModerators()
        {
            var role = this.RoleManager
                .Roles
                .FirstOrDefault(r => r.Name == "Moderator");

            var users = this.UnitOfWork
                .UserRepository
                .Select(u => u.Roles.Any(
                    r => r.RoleId == role.Id));

            var moderators = users.Select(m => new ModeratorBindingModel()
            {
                Id = m.Id,
                Username = m.UserName
            });

            return moderators;
        }
    }
}