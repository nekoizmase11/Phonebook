using Lucene.Net.Search;
using Lucene.Net.Store;
using Phonebook.Bussines.Infrastructure.LuceneSearch.LuceneIndexingService;
using Phonebook.Bussines.Infrastructure.LuceneSearch.LuceneSearchService.Extensions;
using Phonebook.Presentation.Contracts.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace Phonebook.Bussines.Infrastructure.LuceneSearch.LuceneSearchService
{
    public class LuceneSearchService : ILuceneSearchService
    {
        private const int MaxResults = 500;
        private readonly FSDirectory _luceneDirectory;

        public LuceneSearchService()
        {
            // initialize the index directory
            _luceneDirectory = FSDirectory.Open(HostingEnvironment.MapPath(LuceneSearchIndexingService.IndexingPath));
        }

        // wrapper for getting search results from IndexSearcher
        public IEnumerable<ContactDto> GetSearchResults(string searchQuery, List<string> listOfChecked)
        {
            using (IndexSearcher searcher = new IndexSearcher(_luceneDirectory))
            {
                return searcher.GetSearchResults(searchQuery, listOfChecked, MaxResults);
            }
        }
    }
}
