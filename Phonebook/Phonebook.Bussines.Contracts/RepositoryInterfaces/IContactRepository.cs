using Phonebook.Bussines.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Bussines.Contracts.RepositoryInterfaces
{
    public interface IContactRepository
    {
        Contact GetById(int id);
        IEnumerable<Contact> GetAll();

        int Add(Contact entity);

        void Remove(Contact entity);

        void Update(Contact entity);
    }
}
