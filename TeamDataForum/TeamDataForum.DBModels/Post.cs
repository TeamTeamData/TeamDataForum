namespace TeamDataForum.DBModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Post model for Entity framework
    /// </summary>
    public class Post
    {
        private ISet<Like> likes;

        public Post()
        {
            this.likes = new HashSet<Like>();
        }
        
        /// <summary>
        /// Primary key
        /// </summary>
        [Key]
        public int CommentId { get; set; }

        /// <summary>
        /// Is post deleted
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Reference to User model
        /// </summary>
        public virtual User Creator { get; set; }

        /// <summary>
        /// Reference to Topic model
        /// </summary>
        public virtual Topic Topic { get; set; }

        /// <summary>
        /// Reference to PostText
        /// </summary>
        public virtual PostText Text { get; set; }

        /// <summary>
        /// Post likes
        /// </summary>
        public virtual ISet<Like> Likes
        {
            get { return this.likes; }

            set { this.likes = value; }
        }
    }
}
