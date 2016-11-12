namespace TeamDataForum.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Topic model for Entity framework
    /// </summary>
    public class Thread
    {
        private const int TopicMaxLength = 300;

        private const string TopicTitleError = "Topic title is required.";
        private const string TopicMaxLengthError = "Topic title cannot be more than 300 symbols.";
        private const string TopicDateError = "Topic date is required.";

        private ISet<Post> posts;

        public Thread()
        {
            this.posts = new HashSet<Post>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        [Key]
        public int ThreadId { get; set; }

        /// <summary>
        /// Topic title
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = TopicTitleError)]
        [MaxLength(TopicMaxLength, ErrorMessage = TopicMaxLengthError)]
        public string Title { get; set; }

        /// <summary>
        /// Creation date
        /// </summary>
        [Required(ErrorMessage = TopicDateError)]
        public DateTime Date { get; set; }

        public int TimesSeen { get; set; }

        /// <summary>
        /// If topic is locked
        /// </summary>
        public bool IsLocked { get; set; }

        /// <summary>
        /// If topic is deleted
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Reference to user model
        /// </summary>
        public virtual User Creator { get; set; }

        /// <summary>
        /// Reference to subforum
        /// </summary>
        public virtual Forum Forum { get; set; }

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
