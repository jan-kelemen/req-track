using ReqTrack.Application.Web.MVC.ViewModels.Extensions.Projects;
using ReqTrack.Application.Web.MVC.ViewModels.Projects;
using ReqTrack.Domain.UseCases.Core.Boundary.Interfaces;
using ReqTrack.Domain.UseCases.Core.Projects;
using ReqTrack.Domain.UseCases.Core.Projects.ResponseModels;

namespace ReqTrack.Application.Web.MVC.Presenters.Projects
{
    public class UpdateProjectPresenter :
        IPresenter<UpdateProjectResponse, ProjectViewModel>,
        IPresenter<GetProjectResponse, ProjectViewModel>
    {
        private GetProjectResponse _getProjectResponse;

        private UpdateProjectResponse _updateProjectResponse;

        private ProjectViewModel _updateProjectViewModel;

        GetProjectResponse IUseCaseOutputBoundary<GetProjectResponse>.ResponseModel { set { _getProjectResponse = value; } }

        UpdateProjectResponse IUseCaseOutputBoundary<UpdateProjectResponse>.ResponseModel { set { _updateProjectResponse = value; } }

        ProjectViewModel IPresenter<GetProjectResponse, ProjectViewModel>.ViewModel
        {
            get
            {
                if (_updateProjectViewModel == null)
                {
                    _updateProjectViewModel = _getProjectResponse.ProjectInfo.ToViewModel();
                }
                return _updateProjectViewModel;
            }
        }

        ProjectViewModel IPresenter<UpdateProjectResponse, ProjectViewModel>.ViewModel
        {
            get
            {
                if (_updateProjectViewModel == null)
                {
                    _updateProjectViewModel = _getProjectResponse.ProjectInfo.ToViewModel();
                }
                return _updateProjectViewModel;
            }
        }
    }
}
