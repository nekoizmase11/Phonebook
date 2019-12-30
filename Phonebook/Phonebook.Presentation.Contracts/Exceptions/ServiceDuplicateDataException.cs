using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Presentation.Contracts.Exceptions
{
    public class ServiceDuplicateDataException : ServiceDataException
    {
        public ServiceDuplicateDataException()
        {
        }

        public ServiceDuplicateDataException(string message) : base(message)
        {
        }

        public ServiceDuplicateDataException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ServiceDuplicateDataException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
