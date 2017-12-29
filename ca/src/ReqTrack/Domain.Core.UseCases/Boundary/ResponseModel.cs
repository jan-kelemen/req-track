using System.Collections.Generic;

namespace ReqTrack.Domain.Core.UseCases.Boundary
{
    public class ResponseModel
    {
        internal ResponseModel(ExecutionStatus status) => Status = status;

        public ExecutionStatus Status { get; }

        public string Message { get; internal set; }

        public IReadOnlyDictionary<string, string> ValidationErrors { get; internal set; }
    }
}
