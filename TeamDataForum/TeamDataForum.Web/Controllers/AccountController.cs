namespace TeamDataForum.Web.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Bases;
    using DBModels;
    using Models.BindingModels.Users;
    using UnitOfWork.Contracts;
    using System.Web;
    using Microsoft.AspNet.Identity.Owin;

    public class AccountController : ForumBaseController
    {
        public AccountController(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        private ApplicationUserManager UserManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
        }

        private ApplicationSignInManager SignInManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<ApplicationSignInManager>(); }
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
        public async Task<ActionResult> Register(UserRegistrationBindingModel user)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Registration", "Account", null);
            }

            User newUser = new User()
            {
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                UserName = user.Username,
            };

            var result = await this.UserManager.CreateAsync(newUser, user.Password);

            if (!result.Succeeded)
            {
                return RedirectToAction("Registration", "Account", null);
            }

            await this.SignInManager.SignInAsync(newUser, false, false);

            return RedirectToAction("Home", "Home", null);
        }

        public ActionResult Login()
        {
            return View();
        }
    }
}