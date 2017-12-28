using System;
using ReqTrack.Domain.Core.Entities;
using ReqTrack.Domain.Core.Entities.Projects;
using ReqTrack.Domain.Core.Entities.Requirements;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.Core.Repositories.Results;

namespace ReqTrack.Persistence.Concrete.MongoDB.Repositories
{
    public class MongoProjectRepository : MongoBaseRepository, IProjectRepository
    {
        public CreateResult<Project> CreateProject(Project project)
        {
            throw new NotImplementedException();
        }

        public ReadResult<Project> ReadProject(Identity id, bool loadRequirements, bool loadUseCases)
        {
            throw new NotImplementedException();
        }

        public ReadResult<Project> ReadProjectRequirements(Identity id, RequirementType type)
        {
            throw new NotImplementedException();
        }

        public UpdateResult<Project> UpdateProject(Identity id, bool updateUseCases)
        {
            throw new NotImplementedException();
        }

        public UpdateResult<Project> UpdateProjectRequirements(Project project, RequirementType type)
        {
            throw new NotImplementedException();
        }

        public DeleteResult<Identity> DeleteProject(Identity id)
        {
            throw new NotImplementedException();
        }
    }
}
