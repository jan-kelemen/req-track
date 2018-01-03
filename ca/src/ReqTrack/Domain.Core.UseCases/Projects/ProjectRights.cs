namespace ReqTrack.Domain.Core.UseCases.Projects
{
    public class ProjectRights
    {
        public string UserId { get; set; }

        public bool CanViewProject { get; set; }

        public bool CanChangeRequirements { get; set; }

        public bool CanChangeUseCases { get; set; }

        public bool CanChangeProjectRights { get; set; }

        public bool IsAdministrator { get; set; }
    }
}
