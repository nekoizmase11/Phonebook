using Phonebook.Bussines.Contracts.DbContextInterface;
using Phonebook.Bussines.Contracts.RepositoryInterfaces;
using Phonebook.Bussines.Contracts.UnitOfWorkInterface;
using Phonebook.Data.Repositories;
using Phonebook.Data.SQLServerUnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Data.SQLServerDbContext
{
    public class SQLServerContext : IContext
    {
        private readonly SqlConnection _connection;
        private IUnitOfWork unitOfWork;

        private IUserRepository userRepository;
        private IContactRepository contactRepository;
        private IChangePasswordRequestRepository changePasswordRequestRepository;

        public SQLServerContext(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString) || string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException("connection", "Connection string must be valid!");

            _connection = new SqlConnection(connectionString);
            _connection.Open();

            userRepository = null;
            contactRepository = null;
        }

        public IUserRepository UserRepository
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepository(this);
                }
                return userRepository;
            }
        }

        public IContactRepository ContactRepository
        {
            get
            {
                if (contactRepository == null)
                {
                    contactRepository = new ContactRepository(this);
                }
                return contactRepository;
            }
        }

        public IChangePasswordRequestRepository ChangePasswordRequestRepository 
        {
            get
            {
                if (changePasswordRequestRepository == null)
                {
                    changePasswordRequestRepository = new ChangePasswordRequestRepository(this);
                }
                return changePasswordRequestRepository;
            }
        }

        //******************************************************

        public SqlCommand CreateCommand()
        {
            SqlCommand command = _connection.CreateCommand();
            if (unitOfWork != null && unitOfWork.CurrentTransaction != null)
                command.Transaction = unitOfWork.CurrentTransaction;

            return command;
        }

        public IUnitOfWork CreateUnitOfWork()
        {
            if (unitOfWork != null && unitOfWork.CurrentTransaction != null)
                throw new InvalidOperationException("Unit of work is already in use!");

            unitOfWork = new UnitOfWork(_connection);
            return unitOfWork;
        }
    }
}
