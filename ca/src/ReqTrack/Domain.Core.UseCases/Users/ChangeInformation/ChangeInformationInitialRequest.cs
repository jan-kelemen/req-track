using System.Collections.Generic;
using ReqTrack.Domain.Core.UseCases.Boundary.Requests;

namespace ReqTrack.Domain.Core.UseCases.Users.ChangeInformation
{
    public class ChangeInformationInitialRequest : RequestModel
    {
        public ChangeInformationInitialRequest(string requestedBy) : base(requestedBy)
        {

        }

        public string UserId { get; set; }

        protected override void ValidateCore(Dictionary<string, string> errors)
        {
            base.ValidateCore(errors);
            if (string.IsNullOrWhiteSpace(UserId) || RequestedBy != UserId)
            {
                errors.Add(
                    nameof(UserId), 
                    "User identifier can't be different from user which requested the change");
            }
        }
    }
}
