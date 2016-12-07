namespace TeamDataForum.Web.Models.ViewModels.Users
{
    public class CurrentUser
    {
        public string Username { get; set; }

        public string[] Roles { get; set; }

        public bool IsRegistered { get; set; }
    }
}