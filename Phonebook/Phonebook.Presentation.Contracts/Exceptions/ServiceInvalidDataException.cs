using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Presentation.Contracts.Exceptions
{
    public class ServiceInvalidDataException : ServiceDataException
    {
        public ServiceInvalidDataException()
        {
        }

        public ServiceInvalidDataException(string message) : base(message)
        {
        }

        public ServiceInvalidDataException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ServiceInvalidDataException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
