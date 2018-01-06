using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ReqTrack.Domain.Core.Entities.ValidationHelpers;

namespace ReqTrack.Application.Web.MVC.ViewModels.Projects
{
    public class ChangeInformationViewModel : ViewModel
    {
        public ChangeInformationViewModel() : this(null, null) { }

        public ChangeInformationViewModel(string userId, string userName) : base(userId, userName)
        {
        }

        [HiddenInput]
        public string ProjectId { get; set; }

        [HiddenInput]
        public string ProjectName { get; set; }

        [DisplayName("Name")]
        [StringLength(ProjectValidationHelper.MaximumNameLength, ErrorMessage = "Name is too long")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [DisplayName("Description")]
        [StringLength(ProjectValidationHelper.MaximumDescriptionLength, ErrorMessage = "Description is too long.")]
        public string Description { get; set; }
    }
}
