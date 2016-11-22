namespace TeamDataForum.Pagination.PaginationModels
{

    /// <summary>
    /// Holds data for how many elements to skip and take
    /// </summary>
    public class SkipTake
    {
        public SkipTake(int skip, int take)
        {
            this.Skip = skip;
            this.Take = take;
        }

        /// <summary>
        /// Elements to skip
        /// </summary>
        public int Skip { get; private set; }

        /// <summary>
        /// Elements to take
        /// </summary>
        public int Take { get; private set; }
    }
}
