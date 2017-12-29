using System.Collections.Generic;

namespace ReqTrack.Domain.Core.UseCases.Boundary
{
    public class ResponseModel
    {
        internal ResponseModel(ExecutionStatus status, IReadOnlyDictionary<string, string> validationErrors = null)
        {
            Status = status;
            ValidationErrors = validationErrors ?? new Dictionary<string, string>();
        }

        public ExecutionStatus Status { get; }

        public IReadOnlyDictionary<string, string> ValidationErrors { get; }
    }
}
