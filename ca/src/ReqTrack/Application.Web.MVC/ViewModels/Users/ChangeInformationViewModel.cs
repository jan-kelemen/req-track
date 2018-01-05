using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ReqTrack.Domain.Core.Entities.ValidationHelpers;

namespace ReqTrack.Application.Web.MVC.ViewModels.Users
{
    public class ChangeInformationViewModel : ViewModel
    {
        public ChangeInformationViewModel(string userId, string userName) : base(userId, userName)
        {
        }

        [DisplayName("Display name")]
        [StringLength(UserValidationHelper.MaximumDisplayNameLength, ErrorMessage = "Display name is too long.")]
        [Required(ErrorMessage = "Display name is required.")]
        public string DisplayName { get; set; }
    }
}
