namespace TeamDataForum.Web.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;
    using Bases;
    using DBModels;
    using Models.BindingModels.Users;
    using Models.ViewModels.Posts;
    using Models.ViewModels.Users;
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

        /// <summary>
        /// Empty Edit for user
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Edit()
        {
            User user = this.GetUser();

            if (user == default(User))
            {
                return RedirectToAction("BadRequest", "Error");
            }

            EditUserBindingModel editUser = new EditUserBindingModel()
            {
                UserNames = new UserNamesBindingModel()
                {
                    Firstname = user.Firstname,
                    Lastname = user.Lastname,
                    Email = user.Email
                },

                UserPassword = new PasswordBindingModel(),

                UserImage = new ImageUserBindingView(),

                UserTown = new TownUserBindingModel()
                {
                    Name = user.Town.Name,
                    Country = user.Town.Country.Name
                }
            };

            return View(editUser);
        }

        /// <summary>
        /// Change password for user
        /// </summary>
        /// <param name="model">PasswordBindingModel</param>
        /// <returns>Status codes</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(PasswordBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = this.GetCurrentUser();

            var result = await this.UserManager
                .ChangePasswordAsync(user.Id, model.Password, model.NewPassword);

            if (!result.Succeeded)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        /// <summary>
        /// View personal information for logged user
        /// </summary>
        /// <returns>View</returns>
        public ActionResult UserStatus()
        {
            UserFullViewModel user = this.UnitOfWork
                .UserRepository
                .Query
                .Where(u => u.UserName == this.HttpContext.User.Identity.Name)
                .Select(u => new UserFullViewModel
                {
                    Firstname = u.Firstname,
                    Lastname = u.Lastname,
                    Email = u.Email,
                    Image = u.Image,
                    Town = u.Town.Name,
                    Country = u.Town.Country.Name,
                    PostsCount = u.Posts.Count,
                    Posts = u.Posts.Select(p => new UserPostViewModel()
                    {
                        PostId = p.PostId,
                        ThreadId = p.Thread.ThreadId,
                        Thread = p.Thread.Title,
                        Text = p.Text.Text
                    })
                    .OrderByDescending(p => p.PostId)
                    .Take(5)
                })
                .FirstOrDefault();

            if (user == default(UserFullViewModel))
            {
                return RedirectToAction("NotFound", "Error");
            }

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeInformation(UserNamesBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = this.GetCurrentUser();

            if (!string.IsNullOrWhiteSpace(model.Firstname))
            {
                user.Firstname = model.Firstname;
            }

            if (!string.IsNullOrWhiteSpace(model.Lastname))
            {
                user.Lastname = model.Lastname;
            }

            if (!string.IsNullOrWhiteSpace(model.Email))
            {
                user.Email = model.Email;
            }

            this.UnitOfWork
                .UserRepository
                .Update(user);

            this.UnitOfWork.SaveChanges();

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        private User GetUser()
        {
            User user = this.UnitOfWork
                .UserRepository
                .Select(u => u.UserName == this.HttpContext.User.Identity.Name,
                new string[] { "Town", "Town.Country" })
                .FirstOrDefault();

            return user;
        }

        private User GetCurrentUser()
        {
            User user = this.UnitOfWork
                .UserRepository
                .Select(u => u.UserName == this.HttpContext.User.Identity.Name)
                .FirstOrDefault();

            return user;
        }
    }
}