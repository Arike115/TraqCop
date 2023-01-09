using System.Data;

namespace TraqCop.auth.Interface
{
    public interface IFindAsync<TEntity>
    where TEntity : class
    {
        /// <summary>
        /// Get a list of entities
        /// </summary>
        /// <returns>Query result</returns>
        Task<IEnumerable<TEntity>> FindAsync(IDbTransaction transaction = null);
    }
}
