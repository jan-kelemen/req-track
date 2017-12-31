using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using ReqTrack.Domain.Core.Entities;
using ReqTrack.Domain.Core.Security;
using ReqTrack.Persistence.Concrete.MongoDB.Database;
using ReqTrack.Persistence.Concrete.MongoDB.Entities;
using ReqTrack.Persistence.Concrete.MongoDB.Extensions.Mapping;
using ReqTrack.Persistence.Concrete.MongoDB.Repositories.Internal;
using AccessViolationException = ReqTrack.Domain.Core.Exceptions.AccessViolationException;

namespace ReqTrack.Persistence.Concrete.MongoDB.Security
{
    internal class SecurityGateway : ISecurityGateway
    {
        private readonly MongoRepository<MongoSecurityRights> _securityRightsRepository;

        public SecurityGateway(MongoReqTrackDatabase database)
        {
            _securityRightsRepository = new MongoRepository<MongoSecurityRights>(database.SecurityRightsCollection);
        }

        public IEnumerable<ProjectRights> GetProjectRights(Identity projectId)
        {
            var filter = Builders<MongoSecurityRights>.Filter.Eq(x => x.ProjectId, projectId.ToMongoIdentity());
            var rights = _securityRightsRepository.Find(filter);

            return rights.Select(x => new ProjectRights(
                x.UserId.ToDomainIdentity(),
                projectId,
                x.CanViewProject,
                x.CanChangeRequirements,
                x.CanChangeUseCases,
                x.CanChangeProjectRights,
                x.IsAdministrator));
        }

        public ProjectRights GetProjectRights(Identity projectId, Identity userId)
        {
            var filter = Builders<MongoSecurityRights>.Filter
                .Where(x => x.ProjectId == projectId.ToMongoIdentity() && x.UserId == userId.ToMongoIdentity());
            var rights = _securityRightsRepository.Find(filter).FirstOrDefault();
            if (rights == null) { throw new AccessViolationException("Project doesn't exit or user has insufficient rights"); }

            return new ProjectRights(
                userId,
                projectId,
                rights.CanViewProject,
                rights.CanChangeRequirements,
                rights.CanChangeUseCases,
                rights.CanChangeProjectRights,
                rights.IsAdministrator);
        }

        public bool ChangeProjectRights(Identity projectId, IEnumerable<ProjectRights> newProjectRights)
        {
            var deleteFilter = Builders<MongoSecurityRights>.Filter.Eq(x => x.ProjectId, projectId.ToMongoIdentity());
            _securityRightsRepository.Delete(deleteFilter);

            var mongoRights = newProjectRights.Select(x => new MongoSecurityRights
            {
                ProjectId = projectId.ToMongoIdentity(),
                UserId = x.UserId.ToMongoIdentity(),
                CanChangeProjectRights = x.CanChangeProjectRights,
                CanChangeRequirements = x.CanChangeRequirements,
                CanChangeUseCases = x.CanChangeUseCases,
                CanViewProject = x.CanViewProject,
                IsAdministrator = x.IsAdministrator,
            });

            foreach (var mongoRight in mongoRights)
            {
                _securityRightsRepository.Create(mongoRight);
            }

            return true;
        }
    }
}
