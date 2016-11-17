namespace TeamDataForum.DBModels
{
    public partial class Town
    {
        private const int TownNameMaxLength = 100;

        private const string TownNameError = "Town name is required.";
        private const string TownNameMaxLengthError = "Town name cannot be more than 100 symbols.";
    }
}
