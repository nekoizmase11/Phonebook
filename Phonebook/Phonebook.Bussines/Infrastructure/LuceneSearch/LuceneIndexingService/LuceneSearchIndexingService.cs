using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Phonebook.Presentation.Contracts.DataTransferObjects;
using Phonebook.Presentation.Contracts.LuceneSearchHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace Phonebook.Bussines.Infrastructure.LuceneSearch.LuceneIndexingService
{
    public class LuceneSearchIndexingService : ILuceneSearchIndexingService
    {
        private readonly FSDirectory _luceneDirectory;
        private readonly DocumentProvider _documentProvider;
        public const string IndexingPath = "~/App_Data/Indexes";

        public LuceneSearchIndexingService(DocumentProvider documentProvider)
        {
            _documentProvider = documentProvider ?? throw new ArgumentNullException(nameof(documentProvider));
            var indexPath = IndexingPath;
            if (indexPath.StartsWith("~"))
            {
                indexPath = HostingEnvironment.MapPath(indexPath);
            }

            _luceneDirectory = FSDirectory.Open(indexPath);

            using (IndexWriter writer = GetIndexWriter())
            {
                writer.Optimize();
                writer.Commit();
            }
        }

        public int GetNumberOfDocuments()
        {
            using (IndexSearcher searcher = new IndexSearcher(_luceneDirectory))
            {
                int numberOfDocuments = searcher.IndexReader.NumDocs();
                return numberOfDocuments;
            }
        }

        public void IndexContact(ContactDto contact)
        {
            IndexDocumentWithoutCommit(_documentProvider.GenerateDocument(contact));
        }

        public void Commit()
        {
            using (IndexWriter writer = GetIndexWriter())
            {
                writer.Optimize();
                writer.Commit();
            }
        }

        public void DeleteAll()
        {
            using (IndexWriter writer = GetIndexWriter())
            {
                writer.DeleteAll();
            }
        }

        public void Rollback()
        {
            using (IndexWriter writer = GetIndexWriter())
            {
                writer.Rollback();
            }
        }

        public void DeleteContact(ContactDto contact)
        {
            using (IndexWriter writer = GetIndexWriter())
            {
                TermQuery searchQuery = new TermQuery(new Term(FieldNames.id, contact.id.ToString()));

                writer.DeleteDocuments(searchQuery);

            }
        }

        private void IndexDocumentWithoutCommit(Document document)
        {
            if (document != null)
            {
                using (IndexWriter writer = GetIndexWriter())
                {
                    writer.AddDocument(document);
                }
            }
        }

        private IndexWriter GetIndexWriter()
            => new IndexWriter(_luceneDirectory, new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30), IndexWriter.MaxFieldLength.UNLIMITED);
    }
}
