using Phonebook.Bussines.Contracts.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Data.SQLServerExceptions
{
    public class SQLServerInvalidDataException : InvalidDataException
    {
        public SQLServerInvalidDataException()
        {
        }

        public SQLServerInvalidDataException(string message) : base(message)
        {
        }

        public SQLServerInvalidDataException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SQLServerInvalidDataException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
