namespace TeamDataForum.Pagination.Contracts
{
    public interface IPaginationFactory
    {
        IPagination CreatePagination(int? currentPage, int elementsToTake, int totalCountElements);
    }
}
