namespace TeamDataForum.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Subforum model for Entity framework
    /// </summary>
    public partial class Forum
    {
        private ISet<Thread> threads;
        private ISet<User> moderators;

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
        [Required(AllowEmptyStrings = false, ErrorMessage = ForumError)]
        [MaxLength(ForumMaxLength, ErrorMessage = ForumMaxLengthError)]
        [Index]
        public string Title { get; set; }

        /// <summary>
        /// Description of subforum - required
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = DescriptionError)]
        [MaxLength(ForumMaxLength, ErrorMessage = DescriptionMaxError)]
        public string Description { get; set; }

        /// <summary>
        /// Creation date
        /// </summary>
        [Required]
        public DateTime Date { get; set; }

        public bool IsDeleted { get; set; }

        /// <summary>
        /// Creator
        /// </summary>
        public virtual User Creator { get; set; }

        /// <summary>
        /// All topics in subforum
        /// </summary>
        public virtual ISet<Thread> Threads
        {
            get { return this.threads; }

            set { this.threads = value; }
        }

        /// <summary>
        /// Moderators for subforum
        /// </summary>
        public virtual ISet<User> Moderators
        {
            get { return this.moderators; }

            set { this.moderators = value; }
        }
    }
}