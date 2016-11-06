namespace TeamDataForum.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Post model for Entity framework
    /// </summary>
    public class Post
    {
        private ISet<Like> likes;
        private ISet<Post> responses;

        public Post()
        {
            this.likes = new HashSet<Like>();
            this.responses = new HashSet<Post>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        [Key]
        public int PostId { get; set; }

        /// <summary>
        /// Is post deleted
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Post creation date
        /// </summary>
        public DateTime PostDate { get; set; }

        /// <summary>
        /// Modified date
        /// </summary>
        public DateTime? ChangeDate { get; set; }

        /// <summary>
        /// The user who changed the post
        /// </summary>
        public virtual User Changer { get; set; }

        /// <summary>
        /// Reference to User model
        /// </summary>
        public virtual User Creator { get; set; }

        /// <summary>
        /// Reference to Topic model
        /// </summary>
        public virtual Thread Thread { get; set; }

        /// <summary>
        /// Reference to PostText
        /// </summary>
        public virtual PostText Text { get; set; }

        /// <summary>
        /// Post to which this post is reponse
        /// </summary>
        public virtual Post ResponseTo { get; set; }

        /// <summary>
        /// Post likes
        /// </summary>
        public virtual ISet<Like> Likes
        {
            get { return this.likes; }

            set { this.likes = value; }
        }

        /// <summary>
        /// Responses to Post
        /// </summary>
        public virtual ISet<Post> Responses
        {
            get { return this.responses; }

            set { this.responses = value; }
        }
    }
}
