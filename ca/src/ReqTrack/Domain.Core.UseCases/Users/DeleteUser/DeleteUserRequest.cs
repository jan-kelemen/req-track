﻿using System.Collections.Generic;
using System.Linq;
using ReqTrack.Domain.Core.UseCases.Boundary;
using ReqTrack.Domain.Core.UseCases.Boundary.Interfaces;
using ReqTrack.Domain.Core.UseCases.Boundary.Requests;

namespace ReqTrack.Domain.Core.UseCases.Users.DeleteUser
{
    public class DeleteUserRequest : RequestModel
    {
        public DeleteUserRequest(string requestedBy) : base(requestedBy)
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
