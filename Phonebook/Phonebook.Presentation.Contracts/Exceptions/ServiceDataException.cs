using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Presentation.Contracts.Exceptions
{
    public class ServiceDataException : Exception
    {
        public ServiceDataException()
        {
        }

        public ServiceDataException(string message) : base(message)
        {
        }

        public ServiceDataException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ServiceDataException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
