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

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(UserLogBindingModel user)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Invalid user or password.");
                return View(user);
            }

            var result = await SignInManager.PasswordSignInAsync(
                user.Username,
                user.Password,
                false,
                shouldLockout: false);

            if (result != SignInStatus.Success)
            {
                ModelState.AddModelError(string.Empty, "Invalid user or password.");
                return View(user);
            }

            return RedirectToAction("Home", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(UserRegistrationBindingModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            User newUser = new User()
            {
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                UserName = user.Username,
                Email = user.Email
            };

            var result = await this.UserManager.CreateAsync(newUser, user.Password);

            if (!result.Succeeded)
            {
                return View(user);
            }

            await this.SignInManager.SignInAsync(newUser, false, false);

            return RedirectToAction("Home", "Home", null);
        }
    }
}