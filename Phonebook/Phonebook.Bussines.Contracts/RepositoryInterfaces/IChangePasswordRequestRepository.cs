using Phonebook.Bussines.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Bussines.Contracts.RepositoryInterfaces
{
    public interface IChangePasswordRequestRepository
    {
        void Add(ChangePasswordRequest entity);
        int GetUserIdByGuid(string guid);
        void Remove(ChangePasswordRequest entity);
    }
}
