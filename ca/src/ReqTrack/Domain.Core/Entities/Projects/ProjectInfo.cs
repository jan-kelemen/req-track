using System;
using System.Collections.Generic;
using System.Text;

namespace ReqTrack.Domain.Core.Entities.Projects
{
    /// <summary>
    /// Represents basic project information.
    /// </summary>
    public class ProjectInfo : Entity<ProjectInfo>
    {
        /// <summary>
        /// Constructs the project information.
        /// </summary>
        /// <param name="id">Identifier of the project.</param>
        /// <param name="name">Name of the project.</param>
        public ProjectInfo(Identity id, string name)
            : base(id)
        {
            Name = name;
        }

        /// <summary>
        /// Name of the project.
        /// </summary>
        public string Name { get; set; }

        public override bool HasSameValueAs(ProjectInfo other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Name == other.Name;
        }
    }
}
