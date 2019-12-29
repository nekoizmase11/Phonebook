using Phonebook.Bussines.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Presentation.Contracts.DataTransferObjects
{
    public class UserDto
    {
        public int id { get; set; }
        public string email { get; set; }
        public string password { get; set; }

        public UserDto(int id, string email, string password)
        {
            this.id = id;
            this.email = email;
            this.password = password;
        }

        public static explicit operator UserDto(User user)
        {
            return new UserDto(user.id, user.email, user.password);
        }

        public static explicit operator User(UserDto user)
        {
            return new User(user.id, user.email, user.password);
        }
    }
}
