using Phonebook.Presentation.Contracts.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Bussines
{
    public interface IBussinesLayer
    {
        IContactService ContactService { get; }
        IUserService UserService { get; }
        IMailService MailService { get; }
        IChangePasswordRequestService ChangePasswordRequestService { get; }
        ISearchService SearchService { get; }
    }
}
