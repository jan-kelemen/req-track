using System.Collections.Generic;
using System.Linq;

namespace ReqTrack.Domain.Core.UseCases.Boundary.Requests
{
    public abstract class RequestModel
    {
        protected RequestModel(string requestedBy) => RequestedBy = requestedBy;

        /// <summary>
        /// Identifier of the user who initiated the requiest
        /// </summary>
        public string RequestedBy { get; }

        public bool Validate(out Dictionary<string, string> errors)
        {
            errors = new Dictionary<string, string>();
            ValidateCore(errors);
            return !errors.Any();
        }

        protected virtual void ValidateCore(Dictionary<string, string> errors)
        {
            ;
        }
    }
}
