namespace ReqTrack.Domain.UseCases.Core.Boundary.Interfaces
{
    /// <summary>
    /// Output boundary for use cases, channel through which use cases communicate with outside.
    /// </summary>
    /// <typeparam name="Response">Response model.</typeparam>
    public interface IUseCaseOutputBoundary<Response>
    {
        /// <summary>
        /// Response of the use case.
        /// </summary>
        Response ResponseModel { set; }
    }
}
