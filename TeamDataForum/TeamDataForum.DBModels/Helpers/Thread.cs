namespace TeamDataForum.DBModels
{
    public partial class Thread
    {
        private const int TopicMaxLength = 300;

        private const string TopicTitleError = "Topic title is required.";
        private const string TopicMaxLengthError = "Topic title cannot be more than 300 symbols.";
        private const string TopicDateError = "Topic date is required.";

    }
}
