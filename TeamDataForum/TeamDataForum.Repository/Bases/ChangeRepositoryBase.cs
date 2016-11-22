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

        /// <summary>
        /// Updates element in database
        /// </summary>
        /// <param name="element">Element to be updated</param>
        /// <returns>Updated element</returns>
        public T Add(T element)
        {
            this.DbSet.Add(element);

            return element;
        }

        /// <summary>
        /// Deletes element from database
        /// </summary>
        /// <param name="element">Element to be deleted</param>
        /// <returns>Deleted element</returns>
        public T Remove(T element)
        {
            if (this.Context.Entry(element).State == EntityState.Detached)
            {
                this.DbSet.Attach(element);
            }

            this.DbSet.Remove(element);

            return element;
        }

        /// <summary>
        /// Updates element
        /// </summary>
        /// <param name="element">Element to be updated</param>
        /// <returns>Updated element</returns>
        public T Update(T element)
        {
            this.DbSet.Attach(element);

            this.Context.Entry(element).State = EntityState.Modified;

            return element;
        }
    }
}
