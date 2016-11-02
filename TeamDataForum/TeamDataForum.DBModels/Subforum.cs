namespace TeamDataForum.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Subforum model for Entity framework
    /// </summary>
    public class Subforum
    {
        private const int SubforumMaxLength = 300;

        private const string SubforumError = "Sub forum title is required.";
        private const string SubforumMaxLengthError = "Sub forum title cannot be more than 300 symbols.";
        private const string DescriptionError = "Subforum description is required.";
        private const string DescriptionMaxError = "Subforum description cannot be more than 300 symbols.";

        private ISet<Topic> topics;
        private ISet<User> moderators;

        public Subforum()
        {
            this.topics = new HashSet<Topic>();
            this.moderators = new HashSet<User>();
        }

        /// <summary>
        /// Primary key
        /// </summary>
        [Key]
        public int SubforumId { get; set; }

        /// <summary>
        /// Title - required, unique
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = SubforumError)]
        [MaxLength(SubforumMaxLength, ErrorMessage = SubforumMaxLengthError)]
        [Index]
        public string Title { get; set; }

        /// <summary>
        /// Description of subforum - required
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = DescriptionError)]
        [MaxLength(300, ErrorMessage = DescriptionMaxError)]
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
        public virtual ISet<Topic> Topics
        {
            get { return this.topics; }

            set { this.topics = value; }
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