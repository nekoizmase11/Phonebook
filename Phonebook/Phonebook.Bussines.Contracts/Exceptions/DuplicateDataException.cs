using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Bussines.Contracts.Exceptions
{
    public class DuplicateDataException : DataException
    {
        public DuplicateDataException()
        {
        }

        public DuplicateDataException(string message) : base(message)
        {
        }

        public DuplicateDataException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DuplicateDataException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
