using System.Data;

namespace TraqCop.auth.Interface
{
    public interface IDeleteAsync<TEntity>
      where TEntity : class
    {
        /// <summary>
        /// Delete an existing entity
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Task</returns>
        Task DeleteAsync(TEntity entity, IDbTransaction transaction = null);
    }
}
