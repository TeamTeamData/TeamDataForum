namespace TeamDataForum.Repository.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Interface for searchable operations
    /// </summary>
    /// <typeparam name="T">T is DBModel</typeparam>
    public interface ISearchableRepository<T> 
        where T : class
    {
        /// <summary>
        /// Checks if there is a specific record for DBModels
        /// </summary>
        /// <param name="where">Usage: Property => Property.PropertyName == Value</param>
        /// <returns>bool</returns>
        bool Any(Expression<Func<T, bool>> where);

        /// <summary>
        /// Returns count of sql search
        /// </summary>
        /// <returns>integer</returns>
        int Count();

        /// <summary>
        /// Returns count of sql search with where
        /// </summary>
        /// <param name="where">Usage: Property => Property.PropertyName == Value</param>
        /// <returns>integer</returns>
        int Count(Expression<Func<T, bool>> where);

        /// <summary>
        /// Return specific DBModel with Id 
        /// </summary>
        /// <param name="id">Id to search for</param>
        /// <returns>T</returns>
        T Find(object id);

        /// <summary>
        /// Returns all results
        /// </summary>
        /// <returns>List of T</returns>
        List<T> Select();

        /// <summary>
        /// Sql select with where
        /// </summary>
        /// <param name="where">Usage: Property => Property.PropertyName == Value</param>
        /// <returns>List of T</returns>
        List<T> Select(Expression<Func<T, bool>> where);

        /// <summary>
        /// Sql select with where
        /// </summary>
        /// <param name="where">Usage: Property => Property.PropertyName == Value</param>
        /// <param name="orderBy">Usage: Query => Query.OrderBy(Property => Property.PropertyName) or 
        /// Query => Query.OrderByDescending(Property => Property.PropertyName)</param>
        /// <returns>List of T</returns>
        List<T> Select(
            Expression<Func<T, bool>> where,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy);

        /// <summary>
        /// Sql select with where
        /// </summary>
        /// <param name="where">Usage: Property => Property.PropertyName == Value</param>
        /// <param name="orderBy">Usage: Query => Query.OrderBy(Property => Property.PropertyName) or 
        /// Query => Query.OrderByDescending(Property => Property.PropertyName)</param>
        /// <param name="skip">Count elements to skip</param>
        /// <param name="take">Count elements to take</param>
        /// <returns>List of T</returns>
        List<T> Select(
            Expression<Func<T, bool>> where,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
            int skip,
            int take);
    }
}