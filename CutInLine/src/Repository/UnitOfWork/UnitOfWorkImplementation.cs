using System;
using System.Data;

namespace CutInLine.Repository
{
    public class UnitOfWorkImplementation : IUnitOfWork, IDisposable
    {
        private readonly IDbConnection _dbConexao;
        private IDbTransaction? _transaction;

        public UnitOfWorkImplementation(IDbConnection dbConexao)
        {
            _dbConexao = dbConexao;
        }

        IDbConnection IUnitOfWork.Connection
        {
            get { return _dbConexao; }
        }

        void IUnitOfWork.Begin()
        {
            _dbConexao.Open();
            _transaction = _dbConexao.BeginTransaction();
        }

        void IUnitOfWork.Commit()
        {
            _transaction?.Commit();
            _dbConexao.Close();
        }

        void IUnitOfWork.Rollback()
        {
            _transaction?.Rollback();
            _dbConexao.Close();
        }

        bool IUnitOfWork.IsActive()
        {
            if (_transaction == null)
                return false;
            else return true;
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _dbConexao?.Close();
            _dbConexao?.Dispose();
        }
    }
}
