﻿using System.Data;
using TraqCop.auth.Interface;

namespace TraqCop.auth.Dapper
{
    public class UnitOfWork : IUnitOfWork
    {
        private object syncRoot = new object();
        private IDbTransaction _dbTransaction;
        private IDbConnection _connection;

        public UnitOfWork(IDbConnection connection)
        {
            _connection = connection;
        }

        public IDapperRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            return new DapperRepository<TEntity>(_connection);
        }

        public IDbTransaction BeginTransaction()
        {
            if (_connection.State != ConnectionState.Open)
                _connection.Open();

            return _dbTransaction = _connection.BeginTransaction();
        }

        public void Commit()
        {
            if (_dbTransaction == null || _dbTransaction?.Connection?.State == ConnectionState.Closed)
                return;

            _dbTransaction?.Commit();
        }

        public void Rollback()
        {
            if (_dbTransaction == null || _dbTransaction?.Connection?.State == ConnectionState.Closed)
                return;

            _dbTransaction?.Rollback();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public virtual void Dispose(bool disposing)
        {
            GC.SuppressFinalize(this);
        }
    }
}
