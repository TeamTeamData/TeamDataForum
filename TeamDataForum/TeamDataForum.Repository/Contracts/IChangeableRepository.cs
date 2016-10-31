namespace TeamDataForum.Repository.Contracts
{
    /// <summary>
    /// Interface for CRUD operations
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IChangeableRepository<T> where T : class
    {
        /// <summary>
        /// Adds new element to Database
        /// </summary>
        /// <param name="element">New element</param>
        /// <returns>Added new element</returns>
        T Add(T element);

        /// <summary>
        /// Deletes element from Database
        /// </summary>
        /// <param name="element">Element to be deleted</param>
        /// <returns>Deleted element</returns>
        T Remove(T element);

        /// <summary>
        /// Saves current operations
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Updates element to Database
        /// </summary>
        /// <param name="element">Element to be updated.</param>
        /// <returns>Updated element.</returns>
        T Update(T element);
    }
}
