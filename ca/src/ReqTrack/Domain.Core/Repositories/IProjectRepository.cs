using ReqTrack.Domain.Core.Entities;
using ReqTrack.Domain.Core.Entities.Projects;
using ReqTrack.Domain.Core.Entities.Requirements;

namespace ReqTrack.Domain.Core.Repositories
{
    public interface IProjectRepository : IRepository
    {
        Identity CreateProject(Project project);

        Project ReadProject(Identity id, bool loadRequirements, bool loadUseCases);

        Project ReadProjectRequirements(Identity id, RequirementType type);

        bool UpdateProject(Project project, bool updateUseCases);

        bool UpdateProjectRequirements(Project project, RequirementType type);

        bool DeleteProject(Identity id);
    }
}
