namespace TeamDataForum.DBModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Resources;

    /// <summary>
    /// Country model for Entity framework
    /// </summary>
    public class Country
    {
        private ICollection<Town> towns;

        public Country()
        {
            this.towns = new HashSet<Town>();
        }
        
        /// <summary>
        /// Primary key
        /// </summary>
        [Key]
        public int CountryId { get; set; }

        /// <summary>
        /// Country name - required
        /// </summary>
        [Required(AllowEmptyStrings = false, 
            ErrorMessageResourceName = nameof(ModelsRes.ErrorCountryNameRequired), 
            ErrorMessageResourceType = typeof(ModelsRes))]
        [MaxLength(NumericValues.PlaceNameMaxLength,
            ErrorMessageResourceName = nameof(ModelsRes.ErrorCountryNameMaxLength),
            ErrorMessageResourceType = typeof(ModelsRes))]
        [Index("UQ_CountryName")]
        public string Name { get; set; }

        public virtual ICollection<Town> Towns
        {
            get { return this.towns; }

            set { this.towns = value; }
        }
    }
}
