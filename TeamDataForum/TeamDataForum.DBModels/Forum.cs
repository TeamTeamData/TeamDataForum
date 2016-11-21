namespace TeamDataForum.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Resources;

    /// <summary>
    /// Subforum model for Entity framework
    /// </summary>
    public class Forum
    {
        private ICollection<Thread> threads;
        private ICollection<User> moderators;

        public Forum()
        {
            this.threads = new HashSet<Thread>();
            this.moderators = new HashSet<User>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        [Key]
        public int ForumId { get; set; }

        /// <summary>
        /// Title - required, unique
        /// </summary>
        [Required(AllowEmptyStrings = false,
            ErrorMessageResourceName = nameof(ModelsRes.ErrorForumTitleRequired),
            ErrorMessageResourceType = typeof(ModelsRes))]
        [MaxLength(NumericValues.TextMaxLength,
            ErrorMessageResourceName = nameof(ModelsRes.ErrorCountryNameMaxLength),
            ErrorMessageResourceType = typeof(ModelsRes))]
        [Index]
        public string Title { get; set; }

        /// <summary>
        /// Description of subforum - required
        /// </summary>
        [Required(AllowEmptyStrings = false,
            ErrorMessageResourceName = nameof(ModelsRes.ErrorForumDescriptionRequired),
            ErrorMessageResourceType = typeof(ModelsRes))]
        [MaxLength(NumericValues.TextMaxLength,
            ErrorMessageResourceName = nameof(ModelsRes.ErrorForumDescriptionMaxLength),
            ErrorMessageResourceType = typeof(ModelsRes))]
        public string Description { get; set; }

        /// <summary>
        /// Creation date
        /// </summary>
        [Required(ErrorMessageResourceName = nameof(ModelsRes.ErrorForumDateRequired),
            ErrorMessageResourceType = typeof(ModelsRes))]
        public DateTime Date { get; set; }

        public bool IsDeleted { get; set; }

        /// <summary>
        /// Creator
        /// </summary>
        public virtual User Creator { get; set; }

        /// <summary>
        /// All topics in subforum
        /// </summary>
        public virtual ICollection<Thread> Threads
        {
            get { return this.threads; }

            set { this.threads = value; }
        }

        /// <summary>
        /// Moderators for subforum
        /// </summary>
        public virtual ICollection<User> Moderators
        {
            get { return this.moderators; }

            set { this.moderators = value; }
        }
    }
}