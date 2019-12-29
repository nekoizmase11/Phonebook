using Lucene.Net.Analysis.Standard;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Phonebook.Presentation.Contracts.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Bussines.Infrastructure.LuceneSearch.LuceneSearchService.Extensions
{
    public static class IndexSearcherExtensions
    {
        // get maxResults from IndexSearcher with search query
        public static IEnumerable<ContactDto> GetSearchResults(this IndexSearcher searcher, string searchQuery, List<string> listOfChecked, int maxResults)
        {
            List<ContactDto> results = new List<ContactDto>();

            try
            {
                // create a query for the search term on the given fields
                Query query = GetQuery(searchQuery, listOfChecked);

                // search the index with given query for maxResults returned
                TopDocs items = searcher.Search(query, maxResults);

                // iterate through the search results to create result list
                // todo rewrite to foreach
                for (int i = 0; i < items.TotalHits; i++)
                {
                    results.Add(new ContactDto(searcher.Doc(items.ScoreDocs[i].Doc)));
                }
            }
            catch
            {
                // todo log the exception
                return Enumerable.Empty<ContactDto>();
            }

            return results;
        }

        public static int Broj(this IndexSearcher isear)
        {
            int a = 4;
            return a;
        }


        // build boolean query for fields, pass a dictionary with field & Occur enum value to map out the occurrences properly
        private static Query GetQuery(string searchQuery, List<string> listOfChecked)
        {
            // build the main query
            var mainQuery = new BooleanQuery();
            // go through each field and create a sub-query to optimize occurrences in each field

            //List<string> lista = new List<string>();


            foreach (var field in listOfChecked) // todo replace this with a iteration of the passed dictionary
            {
                // build a parser for given field
                var parser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, field, new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30));
                // add parsed query with term* to allow partial matches with all occurrences as should
                mainQuery.Add(parser.Parse($"{searchQuery}*"), Occur.SHOULD);
            }

            // return a built query
            return mainQuery;
        }
    }
}
