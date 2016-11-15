namespace TeamDataForum.Web.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Bases;
    using DBModels;
    using Models.BindingModels.Users;
    using UnitOfWork.Contracts;
    using System.Net;

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

            // to do

            var userManager = new ApplicationUserManager(new UserStore<User>());

            var result = await userManager.CreateAsync(newUser, user.Password);

            if (result.Succeeded)
            {
               // to do
            }

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
    }
}