namespace TeamDataForum.Repository.Bases
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using Contracts;
    using DB;

    /// <summary>
    /// Base class for Repository
    /// </summary>
    /// <typeparam name="T">T is class from DBModels</typeparam>
    public abstract class SearchRepositoryBase<T> : ISearchableRepository<T>
        where T : class
    {
        private const string ContextNullError = "Context cannot be null.";

        private TeamDataForumContext context;
        private DbSet<T> dbSet;

        protected SearchRepositoryBase(TeamDataForumContext context)
        {
            this.Context = context;
            this.DbSet = this.Context.Set<T>();
        }

        protected TeamDataForumContext Context
        {
            get { return this.context; }

            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(ContextNullError);
                }

                this.context = value;
            }
        }

        protected DbSet<T> DbSet
        {
            get { return this.dbSet; }

            private set { this.dbSet = value; }
        }

        public bool Any(Expression<Func<T, bool>> where)
        {
            return this.DbSet
                .Any(where);
        }

        public int Count()
        {
            return this.DbSet
                .Count();
        }

        public int Count(Expression<Func<T, bool>> where)
        {
            return this.DbSet
                .Count(where);
        }

        public T Find(object id)
        {
            return this.DbSet
                .Find(id);
        }

        public List<T> Select(Expression<Func<T, bool>> where)
        {
            return this.DbSet
                .Where(where)
                .ToList();
        }

        public List<T> Select(Expression<Func<T, bool>> where,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy)
        {
            IQueryable<T> query = this.DbSet
                .Where(where);

            query = orderBy(query);

            return query.ToList();
        }

        public List<T> Select(Expression<Func<T, bool>> where,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
            int skip,
            int take)
        {
            IQueryable<T> query = this.DbSet
                .Where(where);

            query = orderBy(query);

            return query
                .Skip(skip)
                .Take(take)
                .ToList();
        }
    }
}
