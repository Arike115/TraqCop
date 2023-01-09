using System.Data;

namespace TraqCop.auth.Interface
{
    public interface IDelete<TEntity>
      where TEntity : class
    {
        /// <summary>
        /// Delete an existing entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Delete(TEntity entity, IDbTransaction transaction = null);
    }
}
