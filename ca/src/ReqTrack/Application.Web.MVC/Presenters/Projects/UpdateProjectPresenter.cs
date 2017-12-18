using ReqTrack.Application.Web.MVC.ViewModels.Projects;
using ReqTrack.Domain.UseCases.Core.Boundary.Interfaces;
using ReqTrack.Domain.UseCases.Core.Projects;

namespace ReqTrack.Application.Web.MVC.Presenters.Projects
{
    public class UpdateProjectPresenter :
        IPresenter<UpdateProjectResponse, UpdateProjectViewModel>,
        IPresenter<GetProjectResponse, UpdateProjectViewModel>
    {
        private GetProjectResponse _getProjectResponse;

        private UpdateProjectResponse _updateProjectResponse;

        private UpdateProjectViewModel _updateProjectViewModel;

        GetProjectResponse IUseCaseOutputBoundary<GetProjectResponse>.ResponseModel { set { _getProjectResponse = value; } }

        UpdateProjectResponse IUseCaseOutputBoundary<UpdateProjectResponse>.ResponseModel { set { _updateProjectResponse = value; } }

        UpdateProjectViewModel IPresenter<GetProjectResponse, UpdateProjectViewModel>.ViewModel
        {
            get
            {
                if (_updateProjectViewModel == null)
                {
                    _updateProjectViewModel = createViewModel(_getProjectResponse);
                }
                return _updateProjectViewModel;
            }
        }

        UpdateProjectViewModel IPresenter<UpdateProjectResponse, UpdateProjectViewModel>.ViewModel
        {
            get
            {
                if (_updateProjectViewModel == null)
                {
                    _updateProjectViewModel = createViewModel(_updateProjectResponse);
                }
                return _updateProjectViewModel;
            }
        }

        private UpdateProjectViewModel createViewModel(GetProjectResponse response)
        {
            return new UpdateProjectViewModel
            {
                Id = response.ProjectInfo.Id,
                Name = response.ProjectInfo.Name,
            };
        }

        private UpdateProjectViewModel createViewModel(UpdateProjectResponse response)
        {
            return new UpdateProjectViewModel
            {
                Id = response.ProjectInfo.Id,
                Name = response.ProjectInfo.Name,
            };
        }
    }
}
