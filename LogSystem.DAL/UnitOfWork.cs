using LogSystem.DAL.Interfaces;
using LogSystem.DAL.Repositories;
using System;
using System.Configuration;
using System.Data;
using System.Data.SQLite;

namespace LogSystem.DAL
{
    public class UnitOfWork: IUnitOfWork
    {
        private IDbConnection _connection = null;
        private IDbTransaction _transaction = null;
        private bool _disposed;

        #region private repositories fields
        private IUserRepository userRepository;
        private IUserActionRepository userActionRepository;
        #endregion

        public UnitOfWork()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["default"].ToString();
            _connection = new SQLiteConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        #region public repositories properties
        public IUserRepository UserRepository
        {
            get { return userRepository ?? (userRepository = new UserRepository(_transaction)); }
        }
        public IUserActionRepository UserActionRepository
        {
            get { return userActionRepository ?? (userActionRepository = new UserActionRepository(_transaction)); }
        }
        #endregion

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                ResetRepositories();
            }
        }

        /// <summary>
        /// set repositories to null after commit
        /// </summary>
        private void ResetRepositories()
        {
            userRepository = null;
            userActionRepository = null;
        }

        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        private void dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _disposed = true;
            }
        }

        ~UnitOfWork()
        {
            dispose(false);
        }
    }
}
