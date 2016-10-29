namespace TeamDataForum.DBModels
{
    using System.ComponentModel.DataAnnotations;

    public class Town
    {
        [Key]
        public int TownId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public virtual Country Country { get; set; }
    }
}
