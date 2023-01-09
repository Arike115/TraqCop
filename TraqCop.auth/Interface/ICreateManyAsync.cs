using System.Data;

namespace TraqCop.auth.Interface
{
    public interface ICreateManyAsync<TEntity>
       where TEntity : class
    {
        /// <summary>
        /// Create a list of new entities
        /// </summary>
        /// <param name="entities">List of entities</param>
        /// <returns>Task</returns>
        Task CreateManyAsync(IEnumerable<TEntity> entities, IDbTransaction transaction = null);
    }
}
