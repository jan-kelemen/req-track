using System;
using System.Collections.Generic;
using System.Text;

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
                    throw new ArgumentException("Name is invalid");
                }

                _name = value;
            }
        }
    }
}
