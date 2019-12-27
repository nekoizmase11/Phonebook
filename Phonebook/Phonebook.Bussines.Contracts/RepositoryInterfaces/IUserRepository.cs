using Phonebook.Bussines.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Bussines.Contracts.RepositoryInterfaces
{
    public interface IUserRepository
    {
        User GetById(int id);
        User GetByEmail(string email);
        IEnumerable<User> GetAll();

        int Add(User entity);

        void Remove(User entity);

        void UpdateUser(User entity);
    }
}
