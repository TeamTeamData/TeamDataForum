namespace TeamDataForum.DBModels
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Post model for Entity framework
    /// </summary>
    public class Post
    {
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
    }
}
