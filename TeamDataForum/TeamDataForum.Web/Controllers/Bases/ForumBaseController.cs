namespace TeamDataForum.Web.Controllers.Bases
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity.Owin;
    using UnitOfWork.Contracts;

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
    }
}