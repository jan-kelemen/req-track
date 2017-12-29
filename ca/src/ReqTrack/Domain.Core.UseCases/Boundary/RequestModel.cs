namespace ReqTrack.Domain.Core.UseCases.Boundary.Interfaces
{
    public class RequestModel
    {
        public RequestModel(string requestedBy) => RequestedBy = null;

        /// <summary>
        /// Identifier of the user who initiated the requiest
        /// </summary>
        public string RequestedBy { get; }
    }
}
