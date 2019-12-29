using Phonebook.Presentation.Contracts.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Bussines.Infrastructure.LuceneSearch.LuceneSearchService
{
    public interface ILuceneSearchService
    {
        IEnumerable<ContactDto> GetSearchResults(string searchQuery, List<string> listOfChecked);
    }
}
