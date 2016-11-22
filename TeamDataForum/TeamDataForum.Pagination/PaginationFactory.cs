namespace TeamDataForum.Pagination
{
    using System;
    using Contracts;

    public class PaginationFactory : IPaginationFactory
    {
        public IPagination CreatePagination(int? currentPage, int elementsToTake, int totalCountElements)
        {
            return new Pagination(currentPage, elementsToTake, totalCountElements);
        }
    }
}