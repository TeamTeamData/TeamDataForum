namespace TeamDataForum.Repository.Contracts
{
    public interface IRepository<T> where T : class, 
        ISearchableRepository<T>, IChangeableRepository<T>
    {
    }
}
