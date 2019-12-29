using Phonebook.Presentation.Contracts.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Bussines.Exceptions
{
    public class DuplicateDataDataSourceException : ServiceDuplicateDataException
    {
        public DuplicateDataDataSourceException()
        {
        }

        public DuplicateDataDataSourceException(string message) : base(message)
        {
        }

        public DuplicateDataDataSourceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DuplicateDataDataSourceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
