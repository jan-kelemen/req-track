using ReqTrack.Domain.UseCases.Core.Boundary.Interfaces;

namespace ReqTrack.Application.Web.MVC.Presenters
{
    /// <summary>
    /// Interface for presenters.
    /// </summary>
    /// <typeparam name="Response">Response model type.</typeparam>
    /// <typeparam name="VM">View model type.</typeparam>
    public interface IPresenter<Response, VM> : IUseCaseOutputBoundary<Response>
    {
        /// <summary>
        /// Gives the appropriate view model for the response.
        /// </summary>
        VM ViewModel { get; }
    }
}
