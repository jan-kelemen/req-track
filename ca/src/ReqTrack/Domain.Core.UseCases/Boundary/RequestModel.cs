using System.Collections.Generic;
using System.Linq;

namespace ReqTrack.Domain.Core.UseCases.Boundary.Interfaces
{
    public abstract class RequestModel
    {
        protected RequestModel(string requestedBy) => RequestedBy = requestedBy;

        /// <summary>
        /// Identifier of the user who initiated the requiest
        /// </summary>
        public string RequestedBy { get; }

        public virtual bool Validate(out Dictionary<string, string> errors)
        {
            errors = new Dictionary<string, string>();

            return !errors.Any();
        }
    }
}
