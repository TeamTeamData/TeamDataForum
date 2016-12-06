namespace TeamDataForum.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Bases;
    using Models.ViewModels.Users;
    using UnitOfWork.Contracts;

    public class ErrorController : ForumBaseController
    {
        public ErrorController(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        [AllowAnonymous]
        public ActionResult NotFound()
        {
            var user = this.GetCurrentUser;

            return this.View(user);
        }

        [AllowAnonymous]
        public ActionResult BadRequest()
        {
            var user = this.GetCurrentUser;

            return this.View(user);
        }
    }
}