namespace TeamDataForum.Web.Controllers
{
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;
    using Bases;
    using DBModels;
    using Models.BindingModels.Users;
    using UnitOfWork.Contracts;

    public class AccountController : ForumBaseController
    {
        public AccountController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        private ApplicationUserManager UserManager
        {
            get { return this.HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
        }

        private ApplicationSignInManager SignInManager
        {
            get { return this.HttpContext.GetOwinContext().GetUserManager<ApplicationSignInManager>(); }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        /// <summary>
        /// Empty login for first time
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Login for checking user, also for errors
        /// </summary>
        /// <param name="user">User to be logged</param>
        /// <returns>View or redirects to home</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(UserLogBindingModel user)
        {
            if (!ModelState.IsValid)
            {
                this.ModelState.AddModelError(string.Empty, "Invalid user or password.");
                return View(user);
            }

            var result = await this.SignInManager.PasswordSignInAsync(
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

        /// <summary>
        /// Empty register controller for first time
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Register controller for register, also shows error data
        /// </summary>
        /// <param name="user">User to be register</param>
        /// <returns>View or redirects to home</returns>
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

        /// <summary>
        /// Controller for sign out
        /// </summary>
        /// <returns>Redirects to home</returns>
        public ActionResult Signout()
        {
            this.AuthenticationManager.SignOut();

            return RedirectToAction("Home", "Home", null);
        }
    }
}