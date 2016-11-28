namespace TeamDataForum.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Bases;
    using UnitOfWork.Contracts;

    public class ErrorController : ForumBaseController
    {
        public ErrorController(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        public ActionResult NotFound()
        {
            return View();
        }

        public ActionResult BadRequest()
        {
            return View();
        }
    }
}