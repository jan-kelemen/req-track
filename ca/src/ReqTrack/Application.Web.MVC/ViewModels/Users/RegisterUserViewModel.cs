using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ReqTrack.Domain.Core.Entities.ValidationHelpers;

namespace ReqTrack.Application.Web.MVC.ViewModels.Users
{
    public class RegisterUserViewModel : ViewModel
    {
        public RegisterUserViewModel() : base(null, null)
        {
        }

        [DisplayName("User name")]
        [StringLength(UserValidationHelper.MaximumUserNameLength, ErrorMessage = "User name is too long.")]
        [Required(ErrorMessage = "User name is required.")]
        public string UserName { get; set; }

        [DisplayName("Display name")]
        [StringLength(UserValidationHelper.MaximumDisplayNameLength, ErrorMessage = "Display name is too long.")]
        [Required(ErrorMessage = "Display name is required.")]
        public string DisplayName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [DisplayName("Confirm password")]
        [Required(ErrorMessage = "Password is required.")]
        public string ConfirmPassword { get; set; }
    }
}
