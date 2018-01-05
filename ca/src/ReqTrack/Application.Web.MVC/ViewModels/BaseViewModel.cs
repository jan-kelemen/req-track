namespace ReqTrack.Application.Web.MVC.ViewModels
{
    public class BaseViewModel
    {
        public BaseViewModel(string userId, string userName)
        {
            ApplicationUserId = userId;
            ApplicationUserName = userName;
        }

        public string ApplicationUserId { get; set; }

        public string ApplicationUserName { get; set; }
    }
}
