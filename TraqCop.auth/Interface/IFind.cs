using System.Data;

namespace TraqCop.auth.Interface
{
    public interface IFind<TEntity>
      where TEntity : class
    {
        /// <summary>
        /// Get a list of entities
        /// </summary>
        /// <returns>Query result</returns>
        IEnumerable<TEntity> Find(IDbTransaction transaction = null);
    }
}
