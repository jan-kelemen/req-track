using ReqTrack.Domain.Core.UseCases.Boundary.Responses;
namespace ReqTrack.Domain.Core.UseCases.Users.ChangeInformation
{
    public class ChangeInformationResponse : ResponseModel
    {
        public string UserId { get; set; }

        public string DisplayName { get; set; }
    }
}
