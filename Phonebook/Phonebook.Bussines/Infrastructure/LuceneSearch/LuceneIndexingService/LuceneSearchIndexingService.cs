using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.Store;
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

        // optimize indexes upon creation of the service
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

        // retrieve number of indexed documents
        public int GetNumberOfDocuments()
        {
            using (IndexSearcher searcher = new IndexSearcher(_luceneDirectory))
            {
                int numberOfDocuments = searcher.IndexReader.NumDocs();
                return numberOfDocuments;
            }
        }

        // index a single contact
        public void IndexContact(ContactTM contact)
        {
            IndexDocumentWithoutCommit(_documentProvider.GenerateDocument(contact));
        }

        // commit the index changes to the directory
        public void Commit()
        {
            using (IndexWriter writer = GetIndexWriter())
            {
                writer.Optimize();
                writer.Commit();
            }
        }

        // use to delete the whole index
        // ie. when creating a rebuild index option, delete all indexes and index each document again
        public void DeleteAll()
        {
            using (IndexWriter writer = GetIndexWriter())
            {
                writer.DeleteAll();
            }
        }

        // use to rollback the index changes in case of an error
        public void Rollback()
        {
            using (IndexWriter writer = GetIndexWriter())
            {
                writer.Rollback();
            }
        }

        // remove a contact from index
        public void DeleteContact(ContactTM contact)
        {
            using (IndexWriter writer = GetIndexWriter())
            {
                // create a query for finding contact with Id
                // todo extract to private method as it is used in multiple places
                TermQuery searchQuery = new TermQuery(new Term(FieldNames.id, contact.id.ToString()));
                // delete all documents matching the query
                writer.DeleteDocuments(searchQuery);

            }
        }

        // add/update a document to/in index 
        private void IndexDocumentWithoutCommit(Document document)
        {
            if (document != null)
            {
                using (IndexWriter writer = GetIndexWriter())
                {
                    // add a document
                    writer.AddDocument(document);
                    // update a single document with given id
                    // todo rewrite to avoid both adding and updating the docs in same call
                    // writer.UpdateDocument(new Term(FieldNames.id, document.Get(FieldNames.id)), document);
                }
            }
        }

        // create an index writer for the current indexing directory
        private IndexWriter GetIndexWriter()
            => new IndexWriter(_luceneDirectory, new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30), IndexWriter.MaxFieldLength.UNLIMITED);
    }
}
