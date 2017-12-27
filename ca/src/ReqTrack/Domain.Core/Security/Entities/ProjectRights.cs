using ReqTrack.Domain.Core.Entities;

namespace ReqTrack.Domain.Core.Security
{
    public class ProjectRights
    {
        private bool _canViewProject;
        private bool _canChangeRequirements;
        private bool _canChangeUseCases;
        private bool _canChangeProjectRights;
        private bool _isAdministrator;

        public ProjectRights(
            Identity userId,
            Identity projectId,
            bool canViewProject,
            bool canChangeRequirements,
            bool canChangeUseCases,
            bool canChangeProjectRights,
            bool isAdministrator)
        {
            UserId = userId;
            ProjectId = projectId;

            CanViewProject = canViewProject;
            CanChangeRequirements = canChangeRequirements;
            CanChangeUseCases = canChangeUseCases;
            CanChangeProjectRights = canChangeProjectRights;
            IsAdministrator = isAdministrator;
        }

        public Identity UserId { get; }

        public Identity ProjectId { get; }

        public bool CanViewProject
        {
            get => _canViewProject;
            private set => _canViewProject = value;
        }

        public bool CanChangeRequirements
        {
            get => _canChangeRequirements;
            private set
            {
                _canChangeRequirements = value;
                if (value)
                {
                    CanViewProject = true;
                }
            }
        }

        public bool CanChangeUseCases
        {
            get => _canChangeUseCases;
            private set
            {
                _canChangeUseCases = value;
                if (value)
                {
                    CanViewProject = true;
                }
            }
        }

        public bool CanChangeProjectRights
        {
            get => _canChangeProjectRights;
            private set
            {
                _canChangeProjectRights = value;
                if (value)
                {
                    CanViewProject = true;
                }
            }
        }

        public bool IsAdministrator
        {
            get => _isAdministrator;
            private set
            {
                _isAdministrator = value;
                if (value)
                {
                    CanChangeProjectRights = true;
                    CanChangeUseCases = true;
                    CanChangeRequirements = true;
                    CanViewProject = true;
                }
            }
        }
    }
}
