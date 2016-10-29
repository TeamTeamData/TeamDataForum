namespace TeamDataForum.DBModels
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Country
    {
        [Key]
        public int CountryID { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Country name is required.")]
        [Index()]
        public string Name { get; set; }
    }
}
