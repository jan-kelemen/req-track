using System.Collections.Generic;
using System.Linq;
using ReqTrack.Domain.Core.Entities.ValidationHelpers;
using ReqTrack.Domain.Core.UseCases.Boundary;
using ReqTrack.Domain.Core.UseCases.Boundary.Interfaces;
using ReqTrack.Domain.Core.UseCases.Boundary.Requests;

namespace ReqTrack.Domain.Core.UseCases.Projects.CreateProject
{
    public class CreateProjectRequest : RequestModel
    {
        public CreateProjectRequest(string requestedBy) : base(requestedBy)
        {
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public override bool Validate(out Dictionary<string, string> errors)
        {
            base.Validate(out errors);

            if (!ProjectValidationHelper.IsNameValid(Name))
            {
                errors.Add(nameof(Name), "Name is invalid");
            }

            if (!ProjectValidationHelper.IsDescriptionValid(Description))
            {
                errors.Add(nameof(Description), "Description is invalid");
            }

            return !errors.Any();
        }
    }
}
