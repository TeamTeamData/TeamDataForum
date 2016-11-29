namespace TeamDataForum.Web.Models.BindingModels.Users
{
    public class EditUserBindingModel
    {
        public UserNamesBindingModel UserNames { get; set; }

        public PasswordBindingModel UserPassword { get; set; }

        public ImageUserBindingView UserImage { get; set; }

        public TownUserBindingModel UserTown { get; set; }
    }
}