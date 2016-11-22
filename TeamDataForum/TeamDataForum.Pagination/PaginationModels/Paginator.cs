namespace TeamDataForum.Pagination.PaginationModels
{

    /// <summary>
    /// Paginator class - represent navigation page
    /// </summary>
    public class Paginator
    {
        public Paginator(string action, string controller, string page, bool isCurrentPage)
        {
            this.Action = action;
            this.Controller = controller;
            this.Page = page;
            this.IsCurrentPage = isCurrentPage;
        }

        /// <summary>
        /// Controller action
        /// </summary>
        public string Action { get; private set; }

        /// <summary>
        /// Controller
        /// </summary>
        public string Controller { get; private set; }

        /// <summary>
        /// Html tag text
        /// </summary>
        public string Page { get; private set; }

        /// <summary>
        /// Is current page for different css style
        /// </summary>
        public bool IsCurrentPage { get; private set; }
    }
}
