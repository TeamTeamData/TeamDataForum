namespace TeamDataForum.DBModels
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Like model for Entity framework
    /// Likes for post
    /// </summary>
    public partial class Like
    {
        /// <summary>
        /// Primary key
        /// </summary>
        [Key]
        public int LikeId { get; set; }

        /// <summary>
        /// Foreign key to Users
        /// </summary>
        [Index(LikeIndex, IsUnique = true, Order = 1)]
        public string UserId { get; set; }
        
        /// <summary>
        /// Reference to User model
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Foreign key to posts
        /// </summary>
        [Index(LikeIndex, IsUnique = true, Order = 2)]
        public int PostId { get; set; }

        /// <summary>
        /// Reference to Post model
        /// </summary>
        public virtual Post Post { get; set; }
    }
}
