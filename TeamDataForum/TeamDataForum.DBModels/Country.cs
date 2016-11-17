namespace TeamDataForum.DBModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Country model for Entity framework
    /// </summary>
    public partial class Country
    {
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

        public virtual ISet<Town> Towns
        {
            get { return this.towns; }

            set { this.towns = value; }
        }
    }
}
