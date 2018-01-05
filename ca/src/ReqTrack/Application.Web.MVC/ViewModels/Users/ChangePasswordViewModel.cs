using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ReqTrack.Application.Web.MVC.ViewModels.Users
{
    public class ChangePasswordViewModel : ViewModel
    {
        public ChangePasswordViewModel(string userId, string userName) : base(userId, userName)
        {
        }

        [DisplayName("Old password")]
        [Required(ErrorMessage = "Password is required.")]
        public string OldPassword { get; set; }

        [DisplayName("New password")]
        [Required(ErrorMessage = "Password is required.")]
        public string NewPassword { get; set; }

        [DisplayName("Confirm passwod")]
        [Required(ErrorMessage = "Password is required.")]
        public string ConfirmedPassword { get; set; }
    }
}
