namespace TeamDataForum.DBModels
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Like model for Entity framework
    /// Likes for post
    /// </summary>
    public class Like
    {
        /// <summary>
        /// Primary key
        /// </summary>
        [Key]
        public int LikeId { get; set; }

        /// <summary>
        /// Reference to User model
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Reference to Post model
        /// </summary>
        public Post Post { get; set; }
    }
}
