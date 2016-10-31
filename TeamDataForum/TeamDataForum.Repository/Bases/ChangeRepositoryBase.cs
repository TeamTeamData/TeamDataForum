namespace TeamDataForum.Repository.Bases
{
    using System.Data.Entity;
    using Contracts;
    using DB;

    /// <summary>
    /// Base class for Repository
    /// </summary>
    /// <typeparam name="T">T is class from DBModels</typeparam>
    public abstract class ChangeRepositoryBase<T> : SearchRepositoryBase<T>, IChangeableRepository<T>
        where T : class
    {
        protected ChangeRepositoryBase(TeamDataForumContext context) 
            : base(context)
        {
        }

        public T Add(T element)
        {
            this.DbSet.Add(element);

            return element;
        }

        public T Remove(T element)
        {
            this.DbSet.Remove(element);

            return element;
        }

        public void SaveChanges()
        {
            this.Context.SaveChanges();
        }

        public T Update(T element)
        {
            this.DbSet.Attach(element);

            this.Context.Entry(element).State = EntityState.Modified;

            return element;
        }
    }
}
