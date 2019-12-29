using Phonebook.Presentation.Contracts.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Presentation.Contracts.ServiceInterfaces
{
    public interface IChangePasswordRequestService
    {
        void SaveRequest(ChangePasswordEmailDto email);

        void ChangeForgottenPassword(ChangedPasswordDto changedPasswordInfo);
    }
}
