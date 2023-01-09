using System.Data;

namespace TraqCop.auth.Interface
{
    public interface IDeleteManyAsync<TEntity>
    where TEntity : class
    {
        /// <summary>
        /// Delete a list of existing entities
        /// </summary>
        /// <param name="entities">Entity list</param>
        /// <returns>Task</returns>
        Task DeleteManyAsync(IEnumerable<TEntity> entities, IDbTransaction transaction = null);
    }
}
