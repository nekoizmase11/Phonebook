using Phonebook.Bussines.Contracts.DbContextInterface;
using Phonebook.Bussines.Services;
using Phonebook.Presentation.Contracts.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Bussines
{
    public class BussinesLayer : IBussinesLayer
    {
        private IContext _context;
        private IContactService contactService;
        private IUserService userService;
        private IMailService mailService;
        private IChangePasswordRequestService changePasswordRequestService;
        private ISearchService searchService;

        public BussinesLayer(IContext ctx)
        {
            _context = ctx;
        }

        public IContactService ContactService
        {
            get
            {
                if (contactService == null)
                {
                    contactService = new ContactService(_context);
                }
                return contactService;
            }
        }

        public IUserService UserService
        {
            get
            {
                if (userService == null)
                {
                    userService = new UserService(_context);
                }
                return userService;
            }
        }

        public IMailService MailService
        {
            get
            {
                if (mailService == null)
                {
                    mailService = new MailService();
                }
                return mailService;
            }
        }

        public IChangePasswordRequestService ChangePasswordRequestService
        {
            get
            {
                if (changePasswordRequestService == null)
                {
                    changePasswordRequestService = new ChangePasswordRequestService(_context);
                }
                return changePasswordRequestService;
            }
        }

        public ISearchService SearchService
        {
            get
            {
                if (searchService == null)
                {
                    searchService = new SearchService();
                }
                return searchService;
            }
        }
    }
}
