namespace TeamDataForum.Pagination.Contracts
{
    using System.Collections.Generic;
    using PaginationModels;

    /// <summary>
    /// Interface for pagination
    /// </summary>
    public interface IPagination
    {
        /// <summary>
        /// Current page
        /// </summary>
        int CurrentPage { get; }

        /// <summary>
        /// Returns Paginator model which holds all pages
        /// </summary>
        /// <param name="action">Controller action</param>
        /// <param name="controller">Controller</param>
        /// <returns>IEnumerable of Paginator</returns>
        IEnumerable<Paginator> GetPages(string action, string controller);

        /// <summary>
        /// Returns the count ot elements to skip and take
        /// </summary>
        /// <returns>SkipTake class</returns>
        SkipTake ElementsToSkipAndTake();
    }
}
