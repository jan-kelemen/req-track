﻿using System.Collections.Generic;
using ReqTrack.Domain.Core.UseCases.Boundary.Requests;
namespace ReqTrack.Domain.Core.UseCases.Users.ViewProfile
{
    public class ViewProfileRequest : RequestModel
    {
        public ViewProfileRequest(string requestedBy) : base(requestedBy)
        {
        }

        public string UserId { get; set; }

        protected override void ValidateCore(Dictionary<string, string> errors)
        {
            base.ValidateCore(errors);
            if (string.IsNullOrWhiteSpace(UserId))
            {
                errors.Add(nameof(UserId), "User identifier is invalid");
            }
        }
    }
}
