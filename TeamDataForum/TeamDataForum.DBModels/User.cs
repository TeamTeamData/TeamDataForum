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
    /// to do add inheritance to identity user
    /// </summary>
    public class User
    {
        private const int NameMaxLength = 50;

        private const string FirstnameError = "First name is required.";
        private const string LastnameError = "Last name is required.";
        private const string NameLengthError = "Name cannot be more than 50 symbols.";

        private ISet<Like> likes;
        private ISet<Post> posts;
        private ISet<Subforum> subforums;
        private ISet<Roles> roles;

        public int UserID { get; set; }

        public User()
        {
            this.roles = new HashSet<Roles>();
            this.likes = new HashSet<Like>();
            this.posts = new HashSet<Post>();
            this.subforums = new HashSet<Subforum>();
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

        public string Telephone { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        /// <summary>
        /// Reference to Town model
        /// </summary>
        public virtual Town Town { get; set; }

        public ISet<Roles> Roles
        {
            get { return this.roles; }

            set { this.roles = value; }
        }

        public ISet<Like> Likes
        {
            get { return this.likes; }

            set { this.likes = value; }
        }

        public ISet<Post> Posts
        {
            get { return this.posts; }

            set { this.posts = value; }
        }

        public ISet<Subforum> Subforums
        {
            get { return this.subforums; }

            set { this.subforums = value; }
        }

       
    }
}
