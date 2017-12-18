using ReqTrack.Application.Web.MVC.ViewModels.Extensions.Projects;
using ReqTrack.Application.Web.MVC.ViewModels.Projects;
using ReqTrack.Domain.UseCases.Core.Boundary.Interfaces;
using ReqTrack.Domain.UseCases.Core.Projects;

namespace ReqTrack.Application.Web.MVC.Presenters.Projects
{
    public class UpdateProjectPresenter :
        IPresenter<UpdateProjectResponse, ProjectInfoViewModel>,
        IPresenter<GetProjectResponse, ProjectInfoViewModel>
    {
        private GetProjectResponse _getProjectResponse;

        private UpdateProjectResponse _updateProjectResponse;

        private ProjectInfoViewModel _updateProjectViewModel;

        GetProjectResponse IUseCaseOutputBoundary<GetProjectResponse>.ResponseModel { set { _getProjectResponse = value; } }

        UpdateProjectResponse IUseCaseOutputBoundary<UpdateProjectResponse>.ResponseModel { set { _updateProjectResponse = value; } }

        ProjectInfoViewModel IPresenter<GetProjectResponse, ProjectInfoViewModel>.ViewModel
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

        ProjectInfoViewModel IPresenter<UpdateProjectResponse, ProjectInfoViewModel>.ViewModel
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
