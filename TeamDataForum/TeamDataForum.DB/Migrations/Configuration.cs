namespace TeamDataForum.DB.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using DBModels;

    internal sealed class Configuration : DbMigrationsConfiguration<TeamDataForumContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(TeamDataForumContext context)
        {
            User user = this.CreateUser(context);
            // to do seed additional models
        }

        /// <summary>
        /// Create administrator
        /// </summary>
        /// <param name="context">TeamDataForumContext</param>
        /// <returns>Returns created administrator</returns>
        private User CreateUser(TeamDataForumContext context)
        {
            string admin = "Administrator";

            if (!context.Users.Any(u => u.UserName == admin))
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());

                IdentityRole adminRole = new IdentityRole()
                {
                    Name = "Administrator"
                };

                IdentityRole moderatorRole = new IdentityRole()
                {
                    Name = "Moderator"
                };

                context.Roles.Add(adminRole);
                context.Roles.Add(moderatorRole);

                var userMananger = new UserManager<User>(new UserStore<User>());
                var passwordHashers = new PasswordHasher();

                User user = new User()
                {
                    Firstname = "Jojo",
                    Lastname = "Mojo",
                    UserName = admin,
                    Town = new Town()
                    {
                        Name = "Sofia",
                        Country = new Country() { Name = "Bulgaria" }
                    },
                    PasswordHash = passwordHashers.HashPassword("123456")
                };

                context.Users.Add(user);

                user.Roles.Add(new IdentityUserRole() { UserId = user.Id, RoleId = adminRole.Id });
                user.Roles.Add(new IdentityUserRole() { UserId = user.Id, RoleId = moderatorRole.Id });

                context.SaveChanges();

                return user;
            }

            return context.Users
                .Where(u => u.UserName == admin)
                .FirstOrDefault();
        }
    }
}
