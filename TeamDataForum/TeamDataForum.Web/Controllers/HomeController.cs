namespace TeamDataForum.Web.Controllers
{
    using System.Web.Mvc;
    using Bases;
    using UnitOfWork.Contracts;

    public class HomeController : ForumBaseController
    {
        public HomeController(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        // GET: Home
        public ActionResult Home()
        {
            return View();
        }
    }
}