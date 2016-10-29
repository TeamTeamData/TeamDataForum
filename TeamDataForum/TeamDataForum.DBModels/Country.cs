namespace TeamDataForum.DBModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Country model for Entity framework
    /// </summary>
    public class Country
    {
        private const int CountryMaxLength = 100;

        private const string ErrorCountryName = "Country name is required.";
        private const string ErrorCountryNameMaxLength = "Country name cannot be more than 100 symbols.";
        private const string CountryNameIndex = "UQ_CountryName";

        private ISet<Town> towns;

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
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorCountryName)]
        [MaxLength(CountryMaxLength, ErrorMessage = ErrorCountryNameMaxLength)]
        [Index(CountryNameIndex)]
        public string Name { get; set; }

        public ISet<Town> Towns
        {
            get { return this.towns; }

            set { this.towns = value; }
        }
    }
}
