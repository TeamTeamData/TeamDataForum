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

        /// <summary>
        /// Required fields - context and DbSet
        /// </summary>
        private TeamDataForumContext context;
        private DbSet<T> dbSet;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">TeamDataForumContext context</param>
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

        /// <summary>
        /// Checks if there is a specific record for DBModels
        /// </summary>
        /// <param name="where">Usage: Property => Property.PropertyName == Value</param>
        /// <returns>bool</returns>
        public bool Any(Expression<Func<T, bool>> where)
        {
            return this.DbSet
                .Any(where);
        }

        /// <summary>
        /// Returns count of sql search
        /// </summary>
        /// <returns>integer</returns>
        public int Count()
        {
            return this.DbSet
                .Count();
        }

        /// <summary>
        /// Returns count of sql search with where
        /// </summary>
        /// <param name="where">Usage: Property => Property.PropertyName == Value</param>
        /// <returns>integer</returns>
        public int Count(Expression<Func<T, bool>> where)
        {
            return this.DbSet
                .Count(where);
        }

        /// <summary>
        /// Return specific DBModel with Id
        /// </summary>
        /// <param name="id">Id to search for</param>
        /// <param name="properties">Enumerable of additional properties to return like "Town.Country"</param>
        /// <returns>T</returns>
        public T Find(object id, IEnumerable<string> properties = null)
        {
            if (properties != null)
            {
                foreach (string property in properties)
                {
                    this.DbSet.Include(property);
                }
            }

            return this.DbSet.Find(id);
        }

        /// <summary>
        /// Returns all results
        /// </summary>
        /// <returns>List of T</returns>
        public List<T> Select()
        {
            return this.DbSet.ToList();
        }

        /// <summary>
        ///  Sql select with where
        /// </summary>
        /// <param name="where">Usage: Property => Property.PropertyName == Value</param>
        /// <param name="properties">Enumerable of additional properties to return like "Town.Country"</param>
        /// <returns>List of T</returns>
        public List<T> Select(
            Expression<Func<T, bool>> where,
            IEnumerable<string> properties = null)
        {
            IQueryable<T> query = this.BuildQuery(where, null, properties);

            return query.ToList();
        }

        /// <summary>
        /// Sql select with where
        /// </summary>
        /// <param name="where">Usage: Property => Property.PropertyName == Value</param>
        /// <param name="orderBy">Usage: Query => Query.OrderBy(Property => Property.PropertyName) or 
        /// Query => Query.OrderByDescending(Property => Property.PropertyName)</param>
        /// <param name="properties">Enumerable of additional properties to return like "Town.Country"</param>
        /// <returns>List of T</returns>
        public List<T> Select(
            Expression<Func<T, bool>> where,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
            IEnumerable<string> properties = null)
        {
            IQueryable<T> query = this.BuildQuery(where, orderBy, properties);

            return query.ToList();
        }

        /// <summary>
        /// Sql select with where
        /// </summary>
        /// <param name="where">Usage: Property => Property.PropertyName == Value</param>
        /// <param name="orderBy">Usage: Query => Query.OrderBy(Property => Property.PropertyName) or 
        /// Query => Query.OrderByDescending(Property => Property.PropertyName)</param>
        /// <param name="skip">Count elements to skip</param>
        /// <param name="take">Count elements to take</param>
        /// <param name="properties">Enumerable of additional properties to return like "Town.Country"</param>
        /// <returns>List of T</returns>
        public List<T> Select(
            Expression<Func<T, bool>> where,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
            int skip,
            int take,
            IEnumerable<string> properties = null)
        {
            IQueryable<T> query = this.BuildQuery(where, orderBy, properties);

            return query
                .Skip(skip)
                .Take(take)
                .ToList();
        }

        /// <summary>
        /// Query builder
        /// </summary>
        /// <param name="where">Usage: Property => Property.PropertyName == Value</param>
        /// <param name="orderBy">Usage: Query => Query.OrderBy(Property => Property.PropertyName) or 
        /// Query => Query.OrderByDescending(Property => Property.PropertyName)</param>
        /// <param name="properties">Enumerable of additional properties to return like "Town.Country"</param>
        /// <returns>Query of T</returns>
        private IQueryable<T> BuildQuery(
            Expression<Func<T, bool>> where,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            IEnumerable<string> properties = null)
        {
            IQueryable<T> query = this.DbSet.Where(where);

            if (properties != null)
            {
                foreach (string property in properties)
                {
                    query.Include(property);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query;
        }
    }
}
