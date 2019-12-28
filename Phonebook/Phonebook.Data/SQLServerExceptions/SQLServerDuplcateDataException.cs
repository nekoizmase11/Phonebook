using Phonebook.Bussines.Contracts.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Data.SQLServerExceptions
{
    public class SQLServerDuplcateDataException : DuplicateDataException
    {
        public SQLServerDuplcateDataException()
        {
        }

        public SQLServerDuplcateDataException(string message) : base(message)
        {
        }

        public SQLServerDuplcateDataException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SQLServerDuplcateDataException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
