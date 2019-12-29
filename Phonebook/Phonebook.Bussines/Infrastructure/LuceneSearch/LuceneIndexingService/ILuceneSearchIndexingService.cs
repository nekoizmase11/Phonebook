using Phonebook.Presentation.Contracts.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Bussines.Infrastructure.LuceneSearch.LuceneIndexingService
{
    public interface ILuceneSearchIndexingService
    {
        void Commit();
        void DeleteAll();
        void DeleteContact(ContactDto contact);
        int GetNumberOfDocuments();
        void IndexContact(ContactDto contact);
        void Rollback();
    }
}
