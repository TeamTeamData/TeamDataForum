namespace TeamDataForum.DBModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Resources;

    /// <summary>
    /// User model for entity framework
    /// </summary>
    public class User : IdentityUser
    {
        private ICollection<Like> likes;
        private ICollection<Post> posts;
        private ICollection<Thread> threads;
        private ICollection<Forum> forums;
        private ICollection<Forum> forumModerators;

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
        [Required(AllowEmptyStrings = false,
            ErrorMessageResourceName = nameof(ModelsRes.ErrorUserFirstnameRequired),
            ErrorMessageResourceType = typeof(ModelsRes))]
        [MaxLength(NumericValues.UsernameMaxLength, 
            ErrorMessageResourceName = nameof(ModelsRes.ErrorUserNameMaxLength),
            ErrorMessageResourceType = typeof(ModelsRes))]
        public string Firstname { get; set; }

        /// <summary>
        /// Last name - required
        /// </summary>
        [Required(AllowEmptyStrings = false,
            ErrorMessageResourceName = nameof(ModelsRes.ErrorUserLastnameRequired),
            ErrorMessageResourceType = typeof(ModelsRes))]
        [MaxLength(NumericValues.UsernameMaxLength,
            ErrorMessageResourceName = nameof(ModelsRes.ErrorUserNameMaxLength),
            ErrorMessageResourceType = typeof(ModelsRes))]
        public string Lastname { get; set; }

        /// <summary>
        /// Avatar of user
        /// </summary>
        [MaxLength(NumericValues.ImagePathMaxLength,
            ErrorMessageResourceName = nameof(ModelsRes.ErrorUserImagePathLength),
            ErrorMessageResourceType = typeof(ModelsRes))]
        public string Image { get; set; }

        /// <summary>
        /// Reference to Town model
        /// </summary>
        public virtual Town Town { get; set; }

        /// <summary>
        /// All user likes
        /// </summary>
        public virtual ICollection<Like> Likes
        {
            get { return this.likes; }

            set { this.likes = value; }
        }

        /// <summary>
        /// All user posts
        /// </summary>
        public virtual ICollection<Post> Posts
        {
            get { return this.posts; }

            set { this.posts = value; }
        }

        /// <summary>
        /// All user created threads
        /// </summary>
        public virtual ICollection<Thread> Threads
        {
            get { return this.threads; }

            set { this.threads = value; }
        }

        /// <summary>
        /// All subforums created by user
        /// </summary>
        public virtual ICollection<Forum> Forums
        {
            get { return this.forums; }

            set { this.forums = value; }
        }

        /// <summary>
        /// All subforums moderate by the user
        /// </summary>
        public virtual ICollection<Forum> ForumModerators
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