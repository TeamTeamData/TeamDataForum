namespace TeamDataForum.DBModels
{
    using System.ComponentModel.DataAnnotations;

    public class Country
    {
        [Key]
        public int CountryID { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Country name is required.")]
        public string Name { get; set; }
    }
}
