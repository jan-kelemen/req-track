using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReqTrack.Domain.Core.UseCases.Boundary;
using ReqTrack.Domain.Core.UseCases.Boundary.Interfaces;
using ReqTrack.Domain.Core.UseCases.Boundary.Requests;

namespace ReqTrack.Domain.Core.UseCases.Users.ViewProfile
{
    public class ViewProfileRequest : RequestModel
    {
        public ViewProfileRequest(string requestedBy) : base(requestedBy)
        {
        }

        public string UserId { get; set; }

        public override bool Validate(out Dictionary<string, string> errors)
        {
            base.Validate(out errors);

            if (string.IsNullOrWhiteSpace(UserId))
            {
                errors.Add(nameof(UserId), "User identifier is invalid");
            }

            return !errors.Any();
        }
    }
}
