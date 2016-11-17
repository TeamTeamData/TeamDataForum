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
    public partial class User : IdentityUser
    {
        private ISet<Like> likes;
        private ISet<Post> posts;
        private ISet<Thread> threads;
        private ISet<Forum> forums;
        private ISet<Forum> forumModerators;

        public User()
        {
            this.likes = new HashSet<Like>();
            this.posts = new HashSet<Post>();
            this.threads = new HashSet<Thread>();
            this.forums = new HashSet<Forum>();
            this.forumModerators = new HashSet<Forum>();
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
        [MaxLength(ImageMaxLength, ErrorMessage = UserImageMaxError)]
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
        public virtual ISet<Forum> Forums
        {
            get { return this.forums; }

            set { this.forums = value; }
        }

        /// <summary>
        /// All subforums moderate by the user
        /// </summary>
        public virtual ISet<Forum> ForumModerators
        {
            get { return this.forumModerators; }

            set { this.forumModerators = value; }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            return userIdentity;
        }
    }
}