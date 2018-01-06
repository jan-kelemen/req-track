using System;
using ReqTrack.Domain.Core.Entities.ValidationHelpers;
using ReqTrack.Domain.Core.Exceptions;

namespace ReqTrack.Domain.Core.Entities.Projects
{
    public class BasicProject : Entity<BasicProject>
    {
        private string _name;

        public BasicProject(Identity id, string name) : base(id)
        {
            Name = name;
        }

        public string Name
        {
            get => _name;
            set
            {
                if (!ProjectValidationHelper.IsNameValid(value))
                {
                    throw new ValidationException("Name is invalid")
                    {
                        PropertyKey = nameof(Name),
                    };
                }

                _name = value;
            }
        }
    }
}
