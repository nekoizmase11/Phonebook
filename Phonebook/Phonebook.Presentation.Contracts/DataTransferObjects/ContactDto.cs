using Lucene.Net.Documents;
using Phonebook.Bussines.Contracts.Models;
using Phonebook.Presentation.Contracts.LuceneSearchHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Presentation.Contracts.DataTransferObjects
{
    public class ContactDto
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }
        public string Number { get; set; }
        public int id_user { get; set; }

        public ContactDto(int id, string name, string lName, string email, string number, int id_user)
        {
            this.id = id;
            Name = name;
            LName = lName;
            Email = email;
            Number = number;
            this.id_user = id_user;
        }

        public ContactDto(Document document)
        {
            this.id = Int32.Parse(document.GetField(FieldNames.id).StringValue);
            Name = document.GetField(FieldNames.Name).StringValue;
            LName = document.GetField(FieldNames.LName).StringValue;
            Email = document.GetField(FieldNames.Email).StringValue;
            Number = document.GetField(FieldNames.Number).StringValue;
            this.id_user = Int32.Parse(document.GetField(FieldNames.id_user).StringValue);
        }

        public static explicit operator ContactDto(Contact contact)
        {
            return new ContactDto(contact.id, contact.Name, contact.LName, contact.Email, contact.Number, contact.id_user);
        }

        public static explicit operator Contact(ContactDto contact)
        {
            return new Contact(contact.id, contact.Name, contact.LName, contact.Email, contact.Number, contact.id_user);
        }
    }
}
