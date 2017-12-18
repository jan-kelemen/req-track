using ReqTrack.Domain.Core.Entities.Requirement;
using System.Collections.Generic;

namespace ReqTrack.Domain.Core.Entities.Project
{
    /// <summary>
    /// Represents a project with requirements and basic information.
    /// </summary>
    public class ProjectWithRequirements : ProjectInfo
    {
        /// <summary>
        /// Basic info of a requirement related to the project.
        /// </summary>
        public class Requirement
        {
            public Requirement(Identity id, RequirementType type, string title, int orderMarker)
            {
                Id = id;
                Type = type;
                Title = title;
                OrderMarker = orderMarker;
            }

            /// <summary>
            /// Identity of the requirement.
            /// </summary>
            public Identity Id { get; private set; }

            /// <summary>
            /// Type of the requirement.
            /// </summary>
            public RequirementType Type { get; private set; }

            /// <summary>
            /// Title of the requirement.
            /// </summary>
            /// <remarks>This field is of a informative character and it's ignored for other purposes.</remarks>
            public string Title { get; private set; }

            /// <summary>
            /// Marker of the position where the requirement appears in the project list.
            /// </summary>
            public int OrderMarker { get; private set; }
        }

        private Dictionary<RequirementType, IList<Requirement>> _requirements 
            = new Dictionary<RequirementType, IList<Requirement>>();
        private Dictionary<Identity, Requirement> _requirementsById 
            = new Dictionary<Identity, Requirement>();

        public ProjectWithRequirements(Identity id, string name, IEnumerable<Requirement> requirements)
            : base(id, name)
        {
            foreach (var r in requirements)
            {
                addRequirement(r);
            }
        }

        /// <summary>
        /// All requirements of the project in arbitrary order.
        /// </summary>
        public IEnumerable<Requirement> Requirements => _requirementsById.Values;

        /// <summary>
        /// Checks if the project has requirement with specified <paramref name="id"/>.
        /// </summary>
        /// <param name="id">identifier of the requirement.</param>
        /// <returns><c>true</c> if the project has the requirement.</returns>
        public bool HasRequirement(Identity id)
        {
            return _requirementsById.ContainsKey(id);
        }

        /// <summary>
        /// Updates the requirements of the project.
        /// </summary>
        /// <param name="requirements">Requirements to update.</param>
        /// <returns>Unprocessed requirements.</returns>
        public IEnumerable<Requirement> UpdateRequirements(IEnumerable<Requirement> requirements)
        {
            var rv = new List<Requirement>();
            foreach(var r in requirements)
            {
                if(HasRequirement(r.Id))
                {
                    updateRequirement(r);
                }
                else
                {
                    rv.Add(r);
                }
            }
            return rv;
        }

        /// <summary>
        /// All requirements of specified type.
        /// </summary>
        /// <param name="type">Type of the searched requirement.</param>
        /// <returns>Associated requirements of specified type.</returns>
        public IList<Requirement> this[RequirementType type]
        {
            get
            {
                if (!_requirements.ContainsKey(type))
                {
                    _requirements.Add(type, new List<Requirement>());
                }
                return _requirements[type];
            }
        }

        /// <summary>
        /// Finds a requirement with matching identity.
        /// </summary>
        /// <param name="id">Identity of the requirement.</param>
        /// <returns>Requirement with matching identity.</returns>
        public Requirement this[Identity id]
        {
            get
            {
                return _requirementsById[id];
            }
        }

        private void addRequirement(Requirement requirement)
        {
            _requirementsById.Add(requirement.Id, requirement);
            var list = this[requirement.Type];
            list.Add(requirement);
        }

        private void updateRequirement(Requirement requirement)
        {
            var existing = this[requirement.Id];

            var typeChanged = existing.Type != requirement.Type;
            if(typeChanged)
            {
                var oldList = this[existing.Type];
                oldList.Remove(requirement);

                var newList = this[requirement.Type];
                newList.Add(requirement);
            }
            _requirementsById[requirement.Id] = requirement;
        }
    }
}
