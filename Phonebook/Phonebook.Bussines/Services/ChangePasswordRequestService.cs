using Phonebook.Bussines.Contracts.DbContextInterface;
using Phonebook.Bussines.Contracts.Exceptions;
using Phonebook.Bussines.Contracts.Models;
using Phonebook.Bussines.Exceptions;
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
    public class ChangePasswordRequestService : IChangePasswordRequestService
    {
        IContext _context;

        public ChangePasswordRequestService(IContext ctx)
        {
            _context = ctx;
        }

        public void SaveRequest(ChangePasswordEmailDto email)
        {
            try
            {
                ChangePasswordRequest request = new ChangePasswordRequest();
                request.Id_Request = email.guid;
                request.Id_user = _context.UserRepository.GetByEmail(email.emailTo).id;
                _context.ChangePasswordRequestRepository.Add(request);
            }
            catch (InvalidDataException ex)
            {
                throw new InvalidDataDataSourceException("Invalid data in service!", ex);
            }
        }

        public void ChangeForgottenPassword(ChangedPasswordDto changedPasswordInfo)
        {
            int userId = _context.ChangePasswordRequestRepository.GetUserIdByGuid(changedPasswordInfo.guid);
            User user = _context.UserRepository.GetById(userId);
            user.password = StringHasher.GenerateHash(changedPasswordInfo.password);

            _context.UserRepository.UpdateUser(user);

            ChangePasswordRequest request = new ChangePasswordRequest();
            request.Id_Request = changedPasswordInfo.guid;

            _context.ChangePasswordRequestRepository.Remove(request);
        }
    }
}
