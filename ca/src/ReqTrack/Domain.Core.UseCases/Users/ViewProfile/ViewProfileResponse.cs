﻿using System.Collections.Generic;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;

namespace ReqTrack.Domain.Core.UseCases.Users.ViewProfile
{
    public class ViewProfileResponse : ResponseModel
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public string DisplayName { get; set; }

        public IEnumerable<Project> Projects { get; set; }
    }
}
