using Phonebook.Presentation.Contracts.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Presentation.Contracts.ServiceInterfaces
{
    public interface IContactService
    {
        ContactDto GetById(int id);
        IEnumerable<ContactDto> ContactsByUser(int userId);

        void CreateContact(ContactDto contact, string userEmail);

        void RemoveContact(int id);

        void UpdateContact(ContactDto contact);
    }
}
