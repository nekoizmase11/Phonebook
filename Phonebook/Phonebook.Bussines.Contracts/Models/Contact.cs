using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Bussines.Contracts.Models
{
    public class Contact
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }
        public string Number { get; set; }
        public int id_user { get; set; }

        public Contact(int id, string name, string lName, string email, string number, int id_user)
        {
            this.id = id;
            Name = name;
            LName = lName;
            Email = email;
            Number = number;
            this.id_user = id_user;
        }
    }
}
