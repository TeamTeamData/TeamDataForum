namespace TeamDataForum.Web.Models.ViewModels.Posts
{
    using System;
    using Users;

    public class PostFullViewModel
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public UserViewModel Author { get; set; }

        public DateTime Date { get; set; }

        public string Changer { get; set; }

        public DateTime? ChangeDate { get; set; }

        public int Number { get; set; }
    }
}