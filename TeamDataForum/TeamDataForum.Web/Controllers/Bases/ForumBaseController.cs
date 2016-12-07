namespace TeamDataForum.Web.Controllers.Bases
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;
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

        protected ApplicationUserManager UserManager
        {
            get { return this.HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
        }

        protected ApplicationSignInManager SignInManager
        {
            get { return this.HttpContext.GetOwinContext().GetUserManager<ApplicationSignInManager>(); }
        }

        protected IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
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

                    var user = this.UserManager
                        .Users
                        .Where(u => u.UserName == currentUser.Username)
                        .FirstOrDefault();

                    currentUser.Roles = this.RoleManager
                        .Roles
                        .Where(r => r.Users.Any(u => u.UserId == user.Id))
                        .Select(r => r.Name)
                        .ToArray();
                }

                return currentUser;
            }
        }
    }
}