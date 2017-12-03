namespace ReqTrack.Application.Web.MVC.Presenters
{
    /// <summary>
    /// Helper class for implementing presenters.
    /// </summary>
    /// <typeparam name="Response">Response model type.</typeparam>
    /// <typeparam name="VM">View model type.</typeparam>
    public abstract class Presenter<Response, VM> : IPresenter<Response, VM>
    {
        private Response _response;

        private VM _viewModel;

        public VM ViewModel
        {
            get
            {
                if(_viewModel == null)
                {
                    _viewModel = CreateViewModel(_response);
                }
                return _viewModel;
            }
        }

        public Response ResponseModel { set { _response = value; } }

        protected abstract VM CreateViewModel(Response response);
    }
}
