namespace TeamDataForum.Repository.Contracts
{
    /// <summary>
    /// Combines ISearchableRepository and IChangeableRepository
    /// </summary>
    /// <typeparam name="T">T is DBModel</typeparam>
    public interface IRepository<T> : ISearchableRepository<T>, IChangeableRepository<T>
        where T : class
    {
    }
}
