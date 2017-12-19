using ReqTrack.Application.Web.MVC.ViewModels.Extensions;
using ReqTrack.Application.Web.MVC.ViewModels.Requirements;
using ReqTrack.Domain.UseCases.Core.Boundary.Interfaces;
using ReqTrack.Domain.UseCases.Core.Requirements.ResponseModels;

namespace ReqTrack.Application.Web.MVC.Presenters.Requirements
{
    public class UpdateRequirementPresenter 
        : IPresenter<GetRequirementResponse, RequirementViewModel>
        , IPresenter<UpdateRequirementResponse, RequirementViewModel>
    {
        private GetRequirementResponse _getRequirementResponse;
        private UpdateRequirementResponse _updateRequirementResponse;
        private RequirementViewModel _updateRequirementViewModel;

        GetRequirementResponse IUseCaseOutputBoundary<GetRequirementResponse>.ResponseModel
        {
            set => _getRequirementResponse = value;
        }
        UpdateRequirementResponse IUseCaseOutputBoundary<UpdateRequirementResponse>.ResponseModel
        {
            set => _updateRequirementResponse = value;
        }

        RequirementViewModel IPresenter<GetRequirementResponse, RequirementViewModel>.ViewModel
        {
            get
            {
                if (_updateRequirementViewModel == null)
                {
                    _updateRequirementViewModel = _getRequirementResponse.Requirement.ToViewModel();
                }
                return _updateRequirementViewModel;
            }
        }

        RequirementViewModel IPresenter<UpdateRequirementResponse, RequirementViewModel>.ViewModel
        {
            get
            {
                if (_updateRequirementViewModel == null)
                {
                    _updateRequirementViewModel = _updateRequirementResponse.Requirement.ToViewModel();
                }
                return _updateRequirementViewModel;
            }
        }
    }
}
