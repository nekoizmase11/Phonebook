using Phonebook.Presentation.Contracts.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Bussines.Exceptions
{
    public class InvalidDataDataSourceException : ServiceInvalidDataException
    {
        public InvalidDataDataSourceException()
        {
        }

        public InvalidDataDataSourceException(string message) : base(message)
        {
        }

        public InvalidDataDataSourceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidDataDataSourceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
