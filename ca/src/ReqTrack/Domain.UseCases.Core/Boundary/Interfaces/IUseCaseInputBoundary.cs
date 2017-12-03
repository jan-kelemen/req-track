namespace ReqTrack.Domain.UseCases.Core.Boundary.Interfaces
{
    /// <summary>
    /// Input boundary for use cases, channel through which others communicate.
    /// </summary>
    /// <typeparam name="Request">Request model.</typeparam>
    /// <typeparam name="Response">Response model.</typeparam>
    public interface IUseCaseInputBoundary<Request, Response>
    {
        /// <summary>
        /// Executes the use case.
        /// </summary>
        /// <param name="outputBoundary">Output boundary.</param>
        /// <param name="requestModel">Request model.</param>
        void Execute(IUseCaseOutputBoundary<Response> outputBoundary, Request requestModel);
    }
}
