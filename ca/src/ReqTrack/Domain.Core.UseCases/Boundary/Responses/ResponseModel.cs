namespace ReqTrack.Domain.Core.UseCases.Boundary.Responses
{
    public abstract class ResponseModel
    {
        internal ResponseModel(ExecutionStatus status) => Status = status;

        public ExecutionStatus Status { get; }

        public string Message { get; internal set; }
    }
}
