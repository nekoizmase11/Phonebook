using Phonebook.Presentation.Contracts.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Presentation.Contracts.ServiceInterfaces
{
    public interface IUserService
    {
        UserDto GetUserByEmail(string email);
        bool Login(string email, string password);
        void RegisterUser(UserDto user);
    }
}
