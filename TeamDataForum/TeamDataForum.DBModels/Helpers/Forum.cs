namespace TeamDataForum.DBModels
{
    public partial class Forum
    {
        private const int ForumMaxLength = 300;

        private const string ForumError = "Forum title is required.";
        private const string ForumMaxLengthError = "Forum title cannot be more than 300 symbols.";
        private const string DescriptionError = "Forum description is required.";
        private const string DescriptionMaxError = "Forum description cannot be more than 300 symbols.";
    }
}
