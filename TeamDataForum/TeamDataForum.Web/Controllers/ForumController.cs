namespace TeamDataForum.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Bases;
    using DBModels;
    using Models.BindingModels.Forums;
    using Models.BindingModels.Users;
    using UnitOfWork.Contracts;

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
            var role = this.GetRoleByName("Moderator");

            ForumBindingModel model = new ForumBindingModel();
            model.Moderators = this.GetUsersByRole(role.Id);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ForumBindingModel forum)
        {
            var role = this.GetRoleByName("Moderator");

            if (!ModelState.IsValid)
            {
                forum.Moderators = this.GetUsersByRole(role.Id);

                return View(forum);
            }

            var moderatorsIds = forum.Moderators
                .Select(m => m.Id)
                .ToArray();

            var users = this.UnitOfWork
                .UserRepository
                .Select(
                u => u.UserName == this.User.Identity.Name || 
                (moderatorsIds.Contains(u.Id) && u.Roles.Any(r => r.RoleId == role.Id)));

            var creator = users.FirstOrDefault(u => u.UserName == this.User.Identity.Name);

            Forum newForum = new Forum()
            {
                Title = forum.Title,
                Description = forum.Description,
                Date = DateTime.Now,
                Creator = creator
            };

            foreach (var user in users)
            {
                newForum.Moderators.Add(user);
            }

            newForum = this.UnitOfWork
                .ForumRepository
                .Add(newForum);

            this.UnitOfWork.SaveChanges();

            // to do change
            return RedirectToAction("Home", "Home", null);
        }

        private ModeratorBindingModel[] GetUsersByRole(string roleId)
        {
            var users = this.UnitOfWork
                .UserRepository
                .Select(u => u.Roles.Any(r => r.RoleId == roleId));

            var moderators = new ModeratorBindingModel[users.Count];

            int counter = 0;

            for (int i = 0; i < users.Count; i++)
            {
                moderators[i] = new ModeratorBindingModel()
                {
                    Id = users[i].Id,
                    Number = counter++,
                    Username = users[i].UserName
                };
            }

            return moderators;
        }

        private IdentityRole GetRoleByName(string roleType)
        {
            var role = this.RoleManager
                .Roles
                .FirstOrDefault(r => r.Name == roleType);

            return role;
        }
    }
}