using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phonebook.Bussines.Contracts.RepositoryInterfaces;

namespace Phonebook.Bussines.Contracts.DbContextInterface
{
    public interface IContext
    {
        IUserRepository UserRep { get; }
        IContactRepository ContactRep { get; }
        IChangePasswordRequestRepository ChangePasswordRequestRep { get; }

    }
}
