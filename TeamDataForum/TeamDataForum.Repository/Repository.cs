namespace TeamDataForum.Repository
{
    using Bases;
    using Contracts;
    using DB;

    /// <summary>
    /// Repository class
    /// </summary>
    /// <typeparam name="T">T is class from DBModels</typeparam>
    public class Repository<T> : ChangeRepositoryBase<T>, IRepository<T>
        where T : class
    {
        public Repository(TeamDataForumContext context) 
            : base(context)
        {
        }
    }
}
