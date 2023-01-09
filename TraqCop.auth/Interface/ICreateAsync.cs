using System.Data;

namespace TraqCop.auth.Interface
{
    public interface ICreateAsync<TEntity>
      where TEntity : class
    {
        /// <summary>
        /// Create a new entity
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Task</returns>
        Task CreateAsync(TEntity entity, IDbTransaction transaction = null);
        Task CreateRoleClaimAsync(TEntity entity, IDbTransaction transaction = null);
        Task CreateRolesClaimAsync(TEntity entity, IDbTransaction transaction = null);
    }
}
