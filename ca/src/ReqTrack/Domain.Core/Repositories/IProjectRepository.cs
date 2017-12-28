using ReqTrack.Domain.Core.Entities;
using ReqTrack.Domain.Core.Entities.Projects;
using ReqTrack.Domain.Core.Entities.Requirements;
using ReqTrack.Domain.Core.Repositories.Results;

namespace ReqTrack.Domain.Core.Repositories
{
    public interface IProjectRepository : IRepository
    {
        CreateResult<Project> CreateProject(Project project);

        ReadResult<Project> ReadProject(Identity id, bool loadRequirements, bool loadUseCases);

        ReadResult<Project> ReadProjectRequirements(Identity id, RequirementType type);

        UpdateResult<Project> UpdateProject(Project id, bool updateUseCases);

        UpdateResult<Project> UpdateProjectRequirements(Project project, RequirementType type);

        DeleteResult<Identity> DeleteProject(Identity id);
    }
}
