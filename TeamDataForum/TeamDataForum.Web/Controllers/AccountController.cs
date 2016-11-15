namespace TeamDataForum.Web.Controllers
{
    using System.Web.Mvc;
    using Bases;
    using Models.BindingModels.Users;
    using UnitOfWork.Contracts;

    public class AccountController : ForumBaseController
    {
        public AccountController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        // GET: Account
        [HttpPost]
        public ActionResult SignUp(UserLogBindingModel user)
        {
            int count = this.UnitOfWork
                .CountryRepository
                .Count();

            return View();
        }

        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserRegistrationBindingModel user)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Registration", "Account", null);
            }

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
    }
}