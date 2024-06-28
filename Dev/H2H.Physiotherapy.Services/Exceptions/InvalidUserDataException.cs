﻿using System;
using System.Runtime.Serialization;

namespace H2H.Physiotherapy.Services.Exceptions
{
    public class InvalidUserDataException : Exception
    {
        public InvalidUserDataException()
        {
        }

        public InvalidUserDataException(string message) : base(message)
        {
        }

        public InvalidUserDataException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidUserDataException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
