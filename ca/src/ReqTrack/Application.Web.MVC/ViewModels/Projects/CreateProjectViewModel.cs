using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ReqTrack.Domain.Core.Entities.ValidationHelpers;

namespace ReqTrack.Application.Web.MVC.ViewModels.Projects
{
    public class CreateProjectViewModel : ViewModel
    {
        public CreateProjectViewModel() : this(null, null) { }

        public CreateProjectViewModel(string userId, string userName) : base(userId, userName) { }

        [DisplayName("Name")]
        [StringLength(ProjectValidationHelper.MaximumNameLength, ErrorMessage = "Name is too long")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [DisplayName("Description")]
        [StringLength(ProjectValidationHelper.MaximumDescriptionLength, ErrorMessage = "Description is too long.")]
        public string Description { get; set; }
    }
}
