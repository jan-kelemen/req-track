using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using MongoDB.Driver;
using ReqTrack.Domain.Core.Entities;
using ReqTrack.Domain.Core.Exceptions;
using ReqTrack.Domain.Core.Security;
using ReqTrack.Domain.Core.Security.Entities;
using ReqTrack.Persistence.Concrete.MongoDB.Database;
using ReqTrack.Persistence.Concrete.MongoDB.Entities;
using ReqTrack.Persistence.Concrete.MongoDB.Extensions.Mapping;
using ReqTrack.Persistence.Concrete.MongoDB.Repositories.Internal;

namespace ReqTrack.Persistence.Concrete.MongoDB.Security
{
    internal class SecurityGateway : ISecurityGateway
    {
        private readonly MongoRepository<MongoSecurityRights> _securityRightsRepository;

        private readonly MongoRepository<MongoUser> _userRepository;

        private readonly MongoRepository<MongoProject> _projectRepository;

        public SecurityGateway(MongoReqTrackDatabase database)
        {
            _securityRightsRepository = new MongoRepository<MongoSecurityRights>(database.SecurityRightsCollection);
            _userRepository = new MongoRepository<MongoUser>(database.UserCollection);
            _projectRepository = new MongoRepository<MongoProject>(database.ProjectCollection);
        }

        public IEnumerable<ProjectRights> GetProjectRights(Identity projectId)
        {
            var project = _projectRepository.Read(projectId.ToMongoIdentity());
            if(project == null) { throw new EntityNotFoundException { Id = projectId } ;}

            var filter = Builders<MongoSecurityRights>.Filter.Eq(x => x.ProjectId, projectId.ToMongoIdentity());
            var rights = _securityRightsRepository.Find(filter);

            return rights.Select(x => new ProjectRights(
                x.UserId.ToDomainIdentity(),
                _userRepository.Read(x.UserId).Username,
                projectId,
                x.CanViewProject,
                x.CanChangeRequirements,
                x.CanChangeUseCases,
                x.CanChangeProjectRights,
                x.IsAdministrator));
        }

        public ProjectRights GetProjectRights(Identity projectId, Identity userId)
        {
            var project = _projectRepository.Read(projectId.ToMongoIdentity());
            if (project == null) { throw new EntityNotFoundException { Id = projectId }; }

            var user = _userRepository.Read(userId.ToMongoIdentity());
            if(user == null) { throw new EntityNotFoundException { Id = userId }; }

            var filter = Builders<MongoSecurityRights>.Filter
                .Where(x => x.ProjectId == projectId.ToMongoIdentity() && x.UserId == userId.ToMongoIdentity());
            var rights = _securityRightsRepository.Find(filter).FirstOrDefault();

            if (rights == null) { return null; }

            return new ProjectRights(
                userId,
                user.Username,
                projectId,
                rights.CanViewProject,
                rights.CanChangeRequirements,
                rights.CanChangeUseCases,
                rights.CanChangeProjectRights,
                rights.IsAdministrator);
        }

        public bool ChangeProjectRights(Identity projectId, IEnumerable<ProjectRights> newProjectRights)
        {
            var project = _projectRepository.Read(projectId.ToMongoIdentity());
            if (project == null) { throw new EntityNotFoundException { Id = projectId }; }

            var filter = Builders<MongoSecurityRights>.Filter.Eq(x => x.ProjectId, projectId.ToMongoIdentity());
            var oldRights = _securityRightsRepository.Find(filter).ToArray();

            _securityRightsRepository.Delete(filter);

            try
            {
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
            }
            catch (EntityNotFoundException)
            {
                foreach (var right in oldRights)
                {
                    _securityRightsRepository.Create(right);
                }

                throw;
            }

            return true;
        }
    }
}
