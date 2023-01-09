using System.Data;

namespace TraqCop.auth.Interface
{
    public interface ICreateMany<TEntity>
     where TEntity : class
    {
        /// <summary>
        /// Create a list of new entities
        /// </summary>
        /// <param name="entities">List of entities</param>
        void CreateMany(IEnumerable<TEntity> entities, IDbTransaction transaction = null);
    }
}
