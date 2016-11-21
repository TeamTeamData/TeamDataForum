namespace TeamDataForum.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Resources;

    /// <summary>
    /// Topic model for Entity framework
    /// </summary>
    public class Thread
    {
        private ICollection<Post> posts;

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
        [Required(AllowEmptyStrings = false,
            ErrorMessageResourceName = nameof(ModelsRes.ErrorThreadTitleRequired),
            ErrorMessageResourceType = typeof(ModelsRes))]
        [MaxLength(NumericValues.TextMaxLength, 
            ErrorMessageResourceName = nameof(ModelsRes.ErrorThreadTitleMaxLength),
            ErrorMessageResourceType = typeof(ModelsRes))]
        public string Title { get; set; }

        /// <summary>
        /// Creation date
        /// </summary>
        [Required(ErrorMessageResourceName = nameof(ModelsRes.ErrorThreadDateRequired),
            ErrorMessageResourceType = typeof(ModelsRes))]
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
        public virtual ICollection<Post> Posts
        {
            get { return this.posts; }

            set { this.posts = value; }
        }
    }
}
