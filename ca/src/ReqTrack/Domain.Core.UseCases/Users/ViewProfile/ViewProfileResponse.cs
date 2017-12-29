using System;
using System.Collections.Generic;
using System.Text;
using ReqTrack.Domain.Core.UseCases.Boundary;

namespace ReqTrack.Domain.Core.UseCases.Users.ViewProfile
{
    public class ViewProfileResponse : ResponseModel
    {
        public class Project
        {
            public string Identifier { get; set; }

            public string Name { get; set; }
        }

        internal ViewProfileResponse(ExecutionStatus status) : base(status)
        {
        }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public string DisplayName { get; set; }

        public IEnumerable<Project> Projects { get; set; }
    }
}
