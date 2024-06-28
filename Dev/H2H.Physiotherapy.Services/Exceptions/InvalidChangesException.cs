using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace H2H.Physiotherapy.Services.Exceptions
{
    public class InvalidChangesException : Exception
    {
        public InvalidChangesException()
        {
        }

        public InvalidChangesException(string message) : base(message)
        {
        }

        public InvalidChangesException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidChangesException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
