using System;
using System.Runtime.Serialization;
using ReqTrack.Domain.Core.Entities;

namespace ReqTrack.Domain.Core.Exceptions
{
    public class AccessViolationException : Exception
    {
        public AccessViolationException()
        {
        }

        public AccessViolationException(string message) : base(message)
        {
        }

        public AccessViolationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AccessViolationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public Identity Id { get; set; }
    }
}
