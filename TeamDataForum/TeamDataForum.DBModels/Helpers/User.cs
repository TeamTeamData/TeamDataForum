namespace TeamDataForum.DBModels
{
    public partial class User
    {
        private const int NameMaxLength = 50;
        private const int ImageMaxLength = 500;

        private const string FirstnameError = "First name is required.";
        private const string LastnameError = "Last name is required.";
        private const string NameLengthError = "Name cannot be more than 50 symbols.";
        private const string UserImageMaxError = "Image path cannot be more than 500 sumbols.";
    }
}
