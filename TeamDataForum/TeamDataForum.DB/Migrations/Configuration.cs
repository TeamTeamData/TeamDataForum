namespace TeamDataForum.DB.Migrations
{
    using System;
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

            Forum subforumRules = this.CreateSubforum(context, user, "Forum Rules", "Team Data Forum forum rules.");
            this.CreateSubforum(context, user, "Forum News", "Forum news.");
            this.CreateSubforum(context, user, "Programming", "Everything about C#, Java, JavaScript and other programming languages.");
            this.CreateSubforum(context, user, "Gaming", "Everything about your favourite games.");
            this.CreateSubforum(context, user, "Off Topic", "Off topic.");

            Thread topicRules = this.CreateTopic(context, user, subforumRules, "Rules", true);

            if (topicRules.Posts.Count == 0)
            {
                this.CreatePost(context, user, topicRules, "Lorem ipsum dolor sit amet, consectetur adipiscing elit." +
                " Sed pulvinar urna sed nisi commodo, et sollicitudin enim consectetur." +
                " Maecenas quis pharetra mi. Interdum et malesuada fames ac ante ipsum primis in faucibus." +
                " Phasellus ullamcorper imperdiet nisi sit amet cursus." +
                " Donec tellus eros, porttitor quis pulvinar id, vehicula ut nisi." +
                "Aenean nec risus eget libero vestibulum vestibulum non id diam. In posuere massa id semper rhoncus." +
                "Sed malesuada, mauris a tempor malesuada, sapien metus dictum purus, at congue neque massa eu urna." +
                "Nullam ornare ligula leo, sit amet sollicitudin urna pharetra sed. ");

                this.CreatePost(context, user, topicRules, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur blandit vitae mauris a auctor." +
                    "Quisque in tincidunt odio. Integer tincidunt lobortis lacus, a viverra ante vestibulum sed. Proin varius porta odio sit amet sollicitudin." +
                    "Vestibulum risus arcu, elementum eget aliquet eget, pharetra ut lorem. Suspendisse vitae eros ac quam dapibus fringilla nec vitae nibh." +
                    "Cras posuere ut nunc at interdum. Aliquam arcu dolor, sollicitudin in neque quis, dapibus rutrum nisl. Sed at facilisis risus." +
                    "Nullam faucibus tortor ut libero maximus facilisis. Aenean urna lorem, facilisis tincidunt pellentesque nec, dignissim at nunc." +
                    "Sed fermentum tempus ex, quis laoreet nisl finibus non. In eget sapien lectus. Aliquam pharetra fringilla nisi non eleifend." +
                    "Vestibulum euismod tincidunt elementum. Nullam pretium lorem sed ornare commodo.");
            } 
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
                    Name = admin
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
                    Firstname = "Admin",
                    Lastname = "Administrator",
                    UserName = admin,
                    Email = "Admin@TeamForumData.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
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

        private Forum CreateSubforum(TeamDataForumContext context, User user, string subforumTitle, string subforumDescription)
        {
            if (!context.Forums.Any(s => s.Title == subforumTitle))
            {
                Forum subforum = new Forum()
                {
                    Title = subforumTitle,
                    Date = DateTime.Now,
                    Description = subforumDescription,
                    Creator = user
                };

                subforum.Moderators.Add(user);

                context.Forums.Add(subforum);

                context.SaveChanges();

                return subforum;
            }

            return context.Forums
                .Where(s => s.Title == subforumTitle)
                .FirstOrDefault();
        }

        private Thread CreateTopic(
            TeamDataForumContext context,
            User user,
            Forum subforum,
            string topicTitle,
            bool isLocked)
        {
            if (!context.Topics.Any(t => t.Title == topicTitle))
            {
                Thread topic = new Thread()
                {
                    Title = topicTitle,
                    Date = DateTime.Now,
                    Creator = user,
                    Forum = subforum,
                    IsLocked = isLocked
                };

                context.Topics.Add(topic);

                context.SaveChanges();

                return topic;
            }

            return context.Topics
                .Where(t => t.Title == topicTitle)
                .FirstOrDefault();
        }

        public Post CreatePost(
            TeamDataForumContext context,
            User user,
            Thread topic,
            string postText)
        {
            Post post = new Post()
            {
                Creator = user,
                PostDate = DateTime.Now,
                Text = new PostText() { Text = postText },
                Thread = topic
            };

            context.Posts.Add(post);

            context.SaveChanges();

            return post;
        }
    }
}
