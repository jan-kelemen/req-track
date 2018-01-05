using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ReqTrack.Domain.Core.Entities.ValidationHelpers;

namespace ReqTrack.Application.Web.MVC.ViewModels.Users
{
    public class LogInViewModel : ViewModel
    {
        public LogInViewModel() : base(null, null)
        {
        }

        [DisplayName("User name")]
        [StringLength(UserValidationHelper.MaximumUserNameLength, ErrorMessage = "User name is invalid.")]
        [Required(ErrorMessage = "User name is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}
