using System.Data;

namespace TraqCop.auth.Interface
{
    public interface ICreate<TEntity>
      where TEntity : class
    {
        /// <summary>
        /// Create a new entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Create(TEntity entity, IDbTransaction transaction = null);
    }
}
