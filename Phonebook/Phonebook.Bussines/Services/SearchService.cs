using Phonebook.Bussines.Infrastructure.LuceneSearch.LuceneIndexingService;
using Phonebook.Bussines.Infrastructure.LuceneSearch.LuceneSearchService;
using Phonebook.Presentation.Contracts.DataTransferObjects;
using Phonebook.Presentation.Contracts.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Bussines.Services
{
    class SearchService : ISearchService
    {
        private readonly ILuceneSearchIndexingService _searchIndexingService;
        private readonly ILuceneSearchService _searchService;

        public SearchService()
        {
            _searchIndexingService = new LuceneSearchIndexingService(new DocumentProvider());
            _searchService = new LuceneSearchService();
        }

        public IEnumerable<ContactDto> SearchListOfContacts(IEnumerable<ContactDto> listToSearch, string searchString, List<string> listOfChecked)
        {



            //index all contacts
            foreach (var item in listToSearch)
            {
                _searchIndexingService.IndexContact(item);
            }

            //search trough those indexed contacts
            IEnumerable<ContactDto> listAfterSearch = _searchService.GetSearchResults(searchString, listOfChecked);

            //delete all indexes
            _searchIndexingService.DeleteAll();


            return listAfterSearch;
        }
    }
}
