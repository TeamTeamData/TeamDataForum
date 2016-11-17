namespace TeamDataForum.DBModels
{
    public partial class Country
    {
        private const int CountryMaxLength = 100;

        private const string ErrorCountryName = "Country name is required.";
        private const string ErrorCountryNameMaxLength = "Country name cannot be more than 100 symbols.";
        private const string CountryNameIndex = "UQ_CountryName";
    }
}
