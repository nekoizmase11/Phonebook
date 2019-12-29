using Phonebook.Bussines.Contracts.DbContextInterface;
using Phonebook.Bussines.Contracts.Exceptions;
using Phonebook.Bussines.Contracts.Models;
using Phonebook.Bussines.Exceptions;
using Phonebook.Presentation.Contracts.DataTransferObjects;
using Phonebook.Presentation.Contracts.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Bussines.Services
{
    public class ContactService : IContactService
    {
        IContext _context;

        public ContactService(IContext ctx)
        {
            _context = ctx;
        }

        public IEnumerable<ContactDto> ContactsByUser(int userId)
        {
            IEnumerable<Contact> contactList = _context.ContactRepository.GetAll().Where(item => item.id_user == userId);
            IEnumerable<ContactDto> contactListDto = contactList.Select(it => (ContactDto)it);

            return contactListDto;
        }

        public ContactDto GetById(int id)
        {
            ContactDto contact = (ContactDto)_context.ContactRepository.GetById(id);

            return contact;
        }

        public void CreateContact(ContactDto contact, string userEmail)
        {
            try
            {
                contact.id_user = _context.UserRepository.GetByEmail(userEmail).id;
                _context.ContactRepository.Add((Contact)contact);
            }
            catch (DuplicateDataException ex)
            {
                throw new DuplicateDataDataSourceException("Duplicate data in service!", ex);
            }
            catch (InvalidDataException ex)
            {
                throw new InvalidDataDataSourceException("Invalid data in service!", ex);
            }
        }

        public void RemoveContact(int id)
        {
            Contact contactToRemove = _context.ContactRepository.GetById(id);
            _context.ContactRepository.Remove(contactToRemove);
        }

        public void UpdateContact(ContactDto contact)
        {
            try
            {
                _context.ContactRepository.Update((Contact)contact);
            }
            catch (DuplicateDataException ex)
            {
                throw new DuplicateDataDataSourceException("Duplicate data in service!", ex);
            }
            catch (InvalidDataException ex)
            {
                throw new InvalidDataDataSourceException("Invalid data in service!", ex);
            }
        }
    }
}
