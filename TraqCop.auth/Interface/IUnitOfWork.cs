using System.Data;

namespace TraqCop.auth.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        void Dispose(bool disposing);
        IDapperRepository<TEntity> Repository<TEntity>() where TEntity : class;
        void Commit();
        void Rollback();
        IDbTransaction BeginTransaction();
    }
}
