namespace TeamDataForum.Repository.Contracts
{
    using System.Linq;

    /// <summary>
    /// Interface for more complex queries. Where Repositories are not otimized.
    /// </summary>
    /// <typeparam name="T">T is DBModel</typeparam>
    public interface IForumQueryable<T> where T : class
    {
        IQueryable<T> Query { get; }
    }
}
