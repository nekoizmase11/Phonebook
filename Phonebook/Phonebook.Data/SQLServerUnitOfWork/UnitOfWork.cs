using Phonebook.Bussines.Contracts.UnitOfWorkInterface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Data.SQLServerUnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private SqlTransaction _transaction;

        public UnitOfWork(SqlConnection connection)
        {
            if (connection.State != ConnectionState.Open)
                throw new ArgumentOutOfRangeException("Connection must be open!", "connection");

            _transaction = connection.BeginTransaction();
        }

        public SqlTransaction CurrentTransaction => _transaction;

        public void Rollback()
        {
            if (_transaction != null)
                _transaction.Rollback();
            else
                throw new InvalidOperationException("You can't rollback when there is no transaction!");

            _transaction = null;
        }

        public void Commit()
        {
            if (_transaction != null)
                _transaction.Commit();
            else
                throw new InvalidOperationException("You can't commit when there is no transaction!");

            _transaction = null;
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }
    }
}
