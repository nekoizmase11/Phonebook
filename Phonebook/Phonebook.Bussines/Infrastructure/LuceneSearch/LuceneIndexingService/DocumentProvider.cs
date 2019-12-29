using Lucene.Net.Documents;
using Phonebook.Presentation.Contracts.DataTransferObjects;
using Phonebook.Presentation.Contracts.LuceneSearchHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Bussines.Infrastructure.LuceneSearch.LuceneIndexingService
{
    public class DocumentProvider
    {
        public Document GenerateDocument(ContactDto contact)
        {
            Document document = new Document();

            document.Add(new Field(FieldNames.id, contact.id.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field(FieldNames.Name, contact.Name, Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field(FieldNames.LName, contact.LName, Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field(FieldNames.Email, contact.Email, Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field(FieldNames.Number, contact.Number, Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field(FieldNames.id_user, contact.id_user.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));

            return document;
        }
    }
}
