using Phonebook.Presentation.Contracts.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Presentation.Contracts.ServiceInterfaces
{
    public interface ISearchService
    {
        IEnumerable<ContactDto> SearchListOfContacts(IEnumerable<ContactDto> lista, string searchString, List<string> listOfChecked);
    }
}
