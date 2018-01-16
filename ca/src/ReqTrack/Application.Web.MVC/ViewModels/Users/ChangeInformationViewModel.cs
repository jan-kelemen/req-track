using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ReqTrack.Domain.Core.Entities.ValidationHelpers;

namespace ReqTrack.Application.Web.MVC.ViewModels.Users
{
    public class ChangeInformationViewModel : ViewModel
    {
        public ChangeInformationViewModel() : this(null, null) { }

        public ChangeInformationViewModel(string userId, string userName) : base(userId, userName)
        {
        }

        [HiddenInput]
        public string UserName { get; set; }

        [DisplayName("Display name")]
        [StringLength(UserValidationHelper.MaximumDisplayNameLength, ErrorMessage = "Display name is too long.")]
        [Required(ErrorMessage = "Display name is required.")]
        public string DisplayName { get; set; }
    }
}
