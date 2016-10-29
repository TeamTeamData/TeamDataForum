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

        private ISet<Topic> topics;

        public Subforum()
        {
            this.topics = new HashSet<Topic>();
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
        /// Creation date
        /// </summary>
        [Required]
        public DateTime Date { get; set; }

        public bool IsDeleted { get; set; }

        /// <summary>
        /// Creator
        /// </summary>
        public User Creator { get; set; }

        /// <summary>
        /// All topics in subforum
        /// </summary>
        public ISet<Topic> Topics
        {
            get { return this.topics; }

            set { this.topics = value; }
        }
    }

}
