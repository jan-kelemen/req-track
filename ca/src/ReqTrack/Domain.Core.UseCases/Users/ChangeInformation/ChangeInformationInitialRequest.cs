using System.Collections.Generic;
using System.Linq;
using ReqTrack.Domain.Core.UseCases.Boundary.Interfaces;

namespace ReqTrack.Domain.Core.UseCases.Users.ChangeInformation
{
    public class ChangeInformationInitialRequest : RequestModel
    {
        public ChangeInformationInitialRequest(string requestedBy) : base(requestedBy)
        {

        }

        public string UserId { get; set; }

        public override bool Validate(out Dictionary<string, string> errors)
        {
            base.Validate(out errors);

            if (string.IsNullOrWhiteSpace(UserId) || RequestedBy != UserId)
            {
                errors.Add(
                    nameof(UserId), 
                    "User identifier can't be different from user which requested the change");
            }

            return !errors.Any();
        }
    }
}
