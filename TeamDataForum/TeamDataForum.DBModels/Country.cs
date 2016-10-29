namespace TeamDataForum.DBModels
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Country model for Entity framework
    /// </summary>
    public class Country
    {
        /// <summary>
        /// Primary key
        /// </summary>
        [Key]
        public int CountryId { get; set; }

        /// <summary>
        /// Country name - required
        /// </summary>
        [Required]
        [MaxLength(100, ErrorMessage = "Country name is required.")]
        [Index()]
        public string Name { get; set; }
    }
}
