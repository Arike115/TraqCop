using System.Data;

namespace TraqCop.auth.Interface
{
    public interface IDeleteMany<TEntity>
      where TEntity : class
    {
        /// <summary>
        /// Delete a list of existing entities
        /// </summary>
        /// <param name="entities">Entity list</param>
        void DeleteMany(IEnumerable<TEntity> entities, IDbTransaction transaction = null);
    }
}
