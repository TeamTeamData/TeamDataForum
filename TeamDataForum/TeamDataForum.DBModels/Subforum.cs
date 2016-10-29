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
        [Required(AllowEmptyStrings = false)]
        [MaxLength(300, ErrorMessage = "Sub forum title is required.")]
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
