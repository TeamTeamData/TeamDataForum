namespace TeamDataForum.DBModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    /// <summary>
    /// User model for entity framework
    /// </summary>
    public class User : IdentityUser
    {
        private const int NameMaxLength = 50;

        private const string FirstnameError = "First name is required.";
        private const string LastnameError = "Last name is required.";
        private const string NameLengthError = "Name cannot be more than 50 symbols.";
        private const string UserImageMaxError = "Image path cannot be more than 500 sumbols.";

        private ISet<Like> likes;
        private ISet<Post> posts;
        private ISet<Thread> threads;
        private ISet<Subforum> subforums;
        private ISet<Subforum> subforumsModerator;

        public User()
        {
            this.likes = new HashSet<Like>();
            this.posts = new HashSet<Post>();
            this.threads = new HashSet<Thread>();
            this.subforums = new HashSet<Subforum>();
            this.subforumsModerator = new HashSet<Subforum>();
        }

        /// <summary>
        /// First name - required
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = FirstnameError)]
        [MaxLength(NameMaxLength, ErrorMessage = NameLengthError)]
        public string Firstname { get; set; }

        /// <summary>
        /// Last name - required
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = LastnameError)]
        [MaxLength(NameMaxLength, ErrorMessage = NameLengthError)]
        public string Lastname { get; set; }

        /// <summary>
        /// Avatar of user
        /// </summary>
        [MaxLength(500, ErrorMessage = UserImageMaxError)]
        public string Image { get; set; }

        /// <summary>
        /// Reference to Town model
        /// </summary>
        public virtual Town Town { get; set; }

        /// <summary>
        /// All user likes
        /// </summary>
        public virtual ISet<Like> Likes
        {
            get { return this.likes; }

            set { this.likes = value; }
        }

        /// <summary>
        /// All user posts
        /// </summary>
        public virtual ISet<Post> Posts
        {
            get { return this.posts; }

            set { this.posts = value; }
        }

        /// <summary>
        /// All user created threads
        /// </summary>
        public virtual ISet<Thread> Threads
        {
            get { return this.threads; }

            set { this.threads = value; }
        }

        /// <summary>
        /// All subforums created by user
        /// </summary>
        public virtual ISet<Subforum> Subforums
        {
            get { return this.subforums; }

            set { this.subforums = value; }
        }

        /// <summary>
        /// All subforums moderate by the user
        /// </summary>
        public virtual ISet<Subforum> SubforumsModerator
        {
            get { return this.subforumsModerator; }

            set { this.subforumsModerator = value; }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            return userIdentity;
        }
    }
}