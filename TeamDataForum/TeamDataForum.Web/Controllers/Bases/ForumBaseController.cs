namespace TeamDataForum.Web.Controllers.Bases
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;
    using Microsoft.AspNet.Identity.Owin;
    using UnitOfWork.Contracts;
    using Models.ViewModels.Users;

    /// <summary>
    /// Base controller for all Mvc Forum controllers
    /// </summary>
    public abstract class ForumBaseController : Controller
    {
        private const string UnitNullError = "Unit of work cannot be null.";

        private IUnitOfWork unitOfWork;

        public ForumBaseController(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        protected IUnitOfWork UnitOfWork
        {
            get { return this.unitOfWork; }

            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(UnitNullError);
                }

                this.unitOfWork = value;
            }
        }

        protected ApplicationRoleManager RoleManager
        {
            get { return this.HttpContext.GetOwinContext().Get<ApplicationRoleManager>(); }
        }

        protected CurrentUser GetCurrentUser
        {
            get
            {
                var currentUser = new CurrentUser();
                currentUser.IsRegistered = this.HttpContext.User.Identity.IsAuthenticated;

                if (currentUser.IsRegistered)
                {
                    currentUser.Username = this.HttpContext.User.Identity.Name;
                    currentUser.Role = Roles.GetRolesForUser();
                }

                return currentUser;
            }
        }
    }
}