using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Bussines.Contracts.Models
{
    public class User
    {
        public int id { get; set; }
        public string email { get; set; }
        public string password { get; set; }

        public User(int id, string email, string password)
        {
            this.id = id;
            this.email = email;
            this.password = password;
        }
    }
}
