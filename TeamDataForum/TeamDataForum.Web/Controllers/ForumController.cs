﻿namespace TeamDataForum.Web.Controllers
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

    [Authorize]
    public class ForumController : ForumBaseController
    {
        public ForumController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        // GET: Forum
        [AllowAnonymous]
        public ActionResult Index()
        {
            return this.RedirectToAction("Home", "Home");
        }

        /// <summary>
        /// Empty create for creating forum
        /// </summary>
        /// <returns>View for creating forum</returns>
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            var role = this.GetRoleByName("Moderator");

            ForumBindingModel model = new ForumBindingModel();
            model.Moderators = this.GetUsersByRole(role.Id);

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create(ForumBindingModel model)
        {
            var role = this.GetRoleByName("Moderator");

            if (!this.ModelState.IsValid)
            {
                model.Moderators = this.GetUsersByRole(role.Id);

                return this.View(model);
            }

            var moderatorsIds = model.Moderators
                .Select(m => m.Id)
                .ToArray();

            var users = this.UnitOfWork
                .UserRepository
                .Select(
                u => u.UserName == this.User.Identity.Name ||
                (moderatorsIds.Contains(u.Id) && u.Roles.Any(r => r.RoleId == role.Id)));

            var creator = users.FirstOrDefault(u => u.UserName == this.User.Identity.Name);

            Forum forum = new Forum()
            {
                Title = model.Title,
                Description = model.Description,
                Date = DateTime.Now,
                Creator = creator
            };

            foreach (var user in users)
            {
                forum.Moderators.Add(user);
            }

            forum = this.UnitOfWork
                .ForumRepository
                .Add(forum);

            this.UnitOfWork.SaveChanges();

            return this.RedirectToAction("View", "Home", new { Id = forum.ForumId });
        }

        /// <summary>
        /// Edit action for forum
        /// </summary>
        /// <param name="id">forum id</param>
        /// <returns>View</returns>
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id)
        {
            Forum forum = this.GetForum(id);

            if (forum == default(Forum) || forum.IsDeleted)
            {
                return this.RedirectToAction("BadRequest", "Error");
            }

            var role = this.GetRoleByName("Moderator");

            var moderators = this.GetUsersByRole(role.Id);

            foreach (var moderator in moderators)
            {
                if (forum.Moderators.Any(m => m.Id == moderator.Id))
                {
                    moderator.IsChecked = true;
                }
            }

            EditForumBindingModel forumModel = new EditForumBindingModel()
            {
                Id = forum.ForumId,
                Title = forum.Title,
                Description = forum.Description,
                Moderators = moderators
            };

            return this.View(forumModel);
        }

        /// <summary>
        /// Action to edit Forum
        /// </summary>
        /// <param name="id">Forum id</param>
        /// <param name="model">EditForumBindingModel</param>
        /// <returns>Redirects</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id, EditForumBindingModel model)
        {
            Forum editForum = this.GetForum(id);

            if (editForum == default(Forum) || editForum.IsDeleted || id != model.Id)
            {
                return this.RedirectToAction("BadRequest", "Error");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var role = this.GetRoleByName("Moderator");

            var moderatorsIds = model.Moderators
                .Select(m => m.Id)
                .ToArray();

            var users = this.UnitOfWork
                .UserRepository
                .Select(u => moderatorsIds.Contains(u.Id) && u.Roles.Any(r => r.RoleId == role.Id));

            string[] addedModerators = editForum.Moderators
                .Select(u => u.UserName)
                .ToArray();

            foreach (string userName in addedModerators)
            {
                if (!users.Any(u => u.UserName == userName))
                {
                    editForum.Moderators.Remove(editForum.Moderators.First(u => u.UserName == userName));
                }
            }

            foreach (User user in users)
            {
                if (!editForum.Moderators.Any(u => u.UserName == user.UserName))
                {
                    editForum.Moderators.Add(user);
                }
            }

            editForum.Title = model.Title;
            editForum.Description = model.Description;

            editForum = this.UnitOfWork
                .ForumRepository
                .Update(editForum);

            this.UnitOfWork.SaveChanges();

            return this.RedirectToAction("View", "Home", new { Id = editForum.ForumId });
        }

        /// <summary>
        /// Delete action for forum
        /// </summary>
        /// <param name="id">Forum id</param>
        /// <returns>View</returns>
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id)
        {
            Forum forum = this.GetForumNoModerators(id);

            if (forum == default(Forum) || forum.IsDeleted)
            {
                return RedirectToAction("BadRequest", "Error");
            }

            DeleteForumBindingModel deleteForum = new DeleteForumBindingModel()
            {
                Id = forum.ForumId,
                Title = forum.Title,
                Description = forum.Description
            };

            return this.View(deleteForum);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id, DeleteForumBindingModel model)
        {
            Forum deleteForum = this.GetForumNoModerators(id);

            if (deleteForum == default(Forum) || deleteForum.IsDeleted || id != model.Id)
            {
                return this.RedirectToAction("BadRequest", "Error");
            }

            deleteForum.IsDeleted = true;

            this.UnitOfWork
                .ForumRepository
                .Update(deleteForum);

            this.UnitOfWork.SaveChanges();

            return this.RedirectToAction("Home", "Home");
        }

        private Forum GetForum(int id)
        {
            Forum forum = this.UnitOfWork
                .ForumRepository
                .Find(id, new string[] { "Moderators" });

            return forum;
        }

        private Forum GetForumNoModerators(int id)
        {
            Forum forum = this.UnitOfWork
                .ForumRepository
                .Find(id);

            return forum;
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