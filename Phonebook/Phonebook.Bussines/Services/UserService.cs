using Phonebook.Bussines.Contracts.DbContextInterface;
using Phonebook.Bussines.Contracts.Models;
using Phonebook.Bussines.Infrastructure;
using Phonebook.Presentation.Contracts.DataTransferObjects;
using Phonebook.Presentation.Contracts.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Bussines.Services
{
    public class UserService : IUserService
    {
        IContext _context;

        public UserService(IContext ctx)
        {
            _context = ctx;
        }

        public UserDto GetUserByEmail(string email)
        {
            User user = _context.UserRepository.GetByEmail(email);
            if (user != null)
            {
                return (UserDto)user;
            }
            else
            {
                return null;
            }

            //return (UserTM)_context.UserRep.GetByEmail(email);
        }

        public bool Login(string email, string password)
        {
            var user = _context.UserRepository.GetByEmail(email);
            if (user != null)
            {
                string hashedPassword = user.password;

                return StringHasher.VerifyHash(password, hashedPassword);
            }
            else
            {
                return false;
            }
        }

        public void RegisterUser(UserDto user)
        {
            user.password = StringHasher.GenerateHash(user.password);
            _context.UserRepository.Add((User)user);

        }
    }
}
