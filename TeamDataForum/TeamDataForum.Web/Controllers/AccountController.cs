namespace TeamDataForum.Web.Controllers
{
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity.Owin;
    using Bases;
    using DBModels;
    using Models.BindingModels.Users;
    using Models.ViewModels.Posts;
    using Models.ViewModels.Users;
    using UnitOfWork.Contracts;
    using System.Web;

    [Authorize]
    public class AccountController : ForumBaseController
    {
        public AccountController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        /// <summary>
        /// Empty login for first time
        /// </summary>
        /// <returns>View</returns>
        [AllowAnonymous]
        public ActionResult Login()
        {
            return this.View();
        }

        /// <summary>
        /// Login for checking user, also for errors
        /// </summary>
        /// <param name="model">User to be logged</param>
        /// <returns>View or redirects to home</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<ActionResult> Login(UserLogBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.ModelState.AddModelError(string.Empty, "Invalid user or password.");

                return this.View(model);
            }

            var result = await this.SignInManager.PasswordSignInAsync(
                model.Username,
                model.Password,
                false,
                shouldLockout: false);

            if (result != SignInStatus.Success)
            {
                this.ModelState.AddModelError(string.Empty, "Invalid user or password.");

                return this.View(model);
            }

            return this.RedirectToAction("Home", "Home");
        }

        /// <summary>
        /// Empty register controller for first time
        /// </summary>
        /// <returns>View</returns>
        [AllowAnonymous]
        public ActionResult Register()
        {
            return this.View();
        }

        /// <summary>
        /// Register controller for register, also shows error data
        /// </summary>
        /// <param name="model">User to be register</param>
        /// <returns>View or redirects to home</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<ActionResult> Register(UserRegistrationBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            User user = new User()
            {
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                UserName = model.Username,
                Email = model.Email
            };

            var result = await this.UserManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return this.View(model);
            }

            await this.SignInManager.SignInAsync(user, false, false);

            return this.RedirectToAction("Home", "Home", null);
        }

        /// <summary>
        /// Controller for sign out
        /// </summary>
        /// <returns>Redirects to home</returns>
        public ActionResult Signout()
        {
            this.AuthenticationManager.SignOut();

            return this.RedirectToAction("Home", "Home", null);
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
                return this.RedirectToAction("BadRequest", "Error");
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
                    Town = user.Town.Name,
                    Country = user.Town.Country.Name
                }
            };

            return this.View(editUser);
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

            User user = this.GetUserNoAdditionalParameters();

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
                    PostsCount = u.Posts.Count(p => !p.IsDeleted),
                    Posts = u.Posts.Where(p => !p.IsDeleted).Select(p => new UserPostViewModel()
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
                return this.RedirectToAction("NotFound", "Error");
            }

            return this.View(user);
        }

        /// <summary>
        /// View information for user with specific id
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>View</returns>
        [HttpGet]
        public ActionResult ViewUser(string id)
        {
            UserFullViewModel user = this.UnitOfWork
                .UserRepository
                .Query
                .Where(u => u.Id == id)
                .Select(u => new UserFullViewModel
                {
                    Firstname = u.Firstname,
                    Lastname = u.Lastname,
                    Email = u.Email,
                    Image = u.Image,
                    Town = u.Town.Name,
                    Country = u.Town.Country.Name,
                    PostsCount = u.Posts.Count(p => !p.IsDeleted),
                    Posts = u.Posts.Where(p => !p.IsDeleted).Select(p => new UserPostViewModel()
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
                return this.RedirectToAction("NotFound", "Error");
            }

            return this.View(user);
        }

        /// <summary>
        /// Cahnges names and email for user
        /// </summary>
        /// <param name="model">UserNamesBindingModel</param>
        /// <returns>Status code</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeInformation(UserNamesBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = this.GetUserNoAdditionalParameters();

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

        /// <summary>
        /// Change town for user
        /// </summary>
        /// <param name="model">TownUserBindingModel</param>
        /// <returns>Status code</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Town(TownUserBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = this.GetUserNoAdditionalParameters();

            Country country = this.UnitOfWork
                .CountryRepository
                .Select(c => c.Name == model.Country, new string[] { "Towns" })
                .FirstOrDefault();

            if (country == default(Country))
            {
                Town newTown = new Town()
                {
                    Name = model.Town,
                    Country = new Country() { Name = model.Country }
                };

                this.UnitOfWork
                    .UserRepository
                    .Update(user);

                this.UnitOfWork.SaveChanges();

                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }

            Town town = country.Towns.FirstOrDefault(t => t.Name == model.Town);

            if (town == default(Town))
            {
                town = new Town()
                {
                    Name = model.Town,
                    Country = country
                };

            }

            user.Town = town;

            this.UnitOfWork
                .UserRepository
                .Update(user);

            this.UnitOfWork.SaveChanges();

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        /// <summary>
        /// Action for uploading user avatar
        /// </summary>
        /// <param name="model">ImageUserBindingView</param>
        /// <returns>Redirects to UserStatus</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadAvatar(ImageUserBindingView model)
        {
            string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };

            if (!this.ModelState.IsValid || model.Image == null)
            {
                return this.RedirectToAction("BadRequest", "Error");
            }

            string extension = Path.GetExtension(model.Image.FileName);

            if (!allowedExtensions.Contains(extension) || model.Image.ContentLength > 512000)
            {
                return this.RedirectToAction("BadRequest", "Error");
            }

            string userName = this.HttpContext.User.Identity.Name;
            string path = $"/Content/Images/Users/{userName}/";
            string defaultPath = $"~{path}";
            string directoryPath = Path.Combine(Server.MapPath(defaultPath));

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string fullPath = directoryPath + userName + extension;

            model.Image.SaveAs(fullPath);

            User user = this.GetUserNoAdditionalParameters();

            user.Image = path + userName + extension;

            this.UnitOfWork
                .UserRepository
                .Update(user);

            this.UnitOfWork
                .SaveChanges();

            return RedirectToAction("UserStatus", "Account");
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

        private User GetUserNoAdditionalParameters()
        {
            User user = this.UnitOfWork
                .UserRepository
                .Select(u => u.UserName == this.HttpContext.User.Identity.Name)
                .FirstOrDefault();

            return user;
        }
    }
}