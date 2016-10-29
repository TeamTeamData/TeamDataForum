namespace TeamDataForum.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Topic model for Entity framework
    /// </summary>
    public class Topic
    {
        private ISet<Post> posts;

        public Topic()
        {
            this.posts = new HashSet<Post>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        [Key]
        public int TopicId { get; set; }

        /// <summary>
        /// Topic title
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MaxLength(300, ErrorMessage = "Topic name is required.")]
        public string Title { get; set; }

        /// <summary>
        /// Creation date
        /// </summary>
        [Required(ErrorMessage = "Topic date is required.")]
        public DateTime Date { get; set; }

        public int TimesSeen { get; set; }

        public bool IsDeleted { get; set; }

        /// <summary>
        /// Reference to user model
        /// </summary>
        public virtual User Creator { get; set; }

        /// <summary>
        /// All topic posts
        /// </summary>
        public virtual ISet<Post> Posts
        {
            get { return this.posts; }

            set { this.posts = value; }
        }
    }
}
