using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ReqTrack.Domain.Core.UseCases.Exceptions
{
    public class RequestValidationException : Exception
    {
        public RequestValidationException()
        {
        }

        public RequestValidationException(string message) : base(message)
        {
        }

        public RequestValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RequestValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public IReadOnlyDictionary<string, string> ValidationErrors { get; internal set; }
    }
}
