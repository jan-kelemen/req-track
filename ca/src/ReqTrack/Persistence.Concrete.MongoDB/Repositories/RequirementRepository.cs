using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using ReqTrack.Domain.Core.Entities;
using ReqTrack.Domain.Core.Entities.Requirements;
using ReqTrack.Domain.Core.Exceptions;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Persistence.Concrete.MongoDB.Database;
using ReqTrack.Persistence.Concrete.MongoDB.Entities;
using ReqTrack.Persistence.Concrete.MongoDB.Extensions.Mapping;

namespace ReqTrack.Persistence.Concrete.MongoDB.Repositories
{
    internal class RequirementRepository : BaseRepository, IRequirementRepository
    {
        public RequirementRepository(MongoReqTrackDatabase database) : base(database)
        {
        }

        public Identity CreateRequirement(Requirement requirement)
        {
            var mongoRequirement = requirement.ToMongoEntity();

            if (_projectRepository.Count(_projectRepository.IdFilter(mongoRequirement.ProjectId)) == 0)
            {
                throw new EntityNotFoundException($"ID={requirement.Project.Id}");
            }

            if (_userRepository.Count(_userRepository.IdFilter(mongoRequirement.AuthorId)) == 0)
            {
                throw new EntityNotFoundException($"ID={requirement.Author.Id}");
            }

            mongoRequirement.OrderMarker = LastOrderMarker(mongoRequirement.ProjectId, mongoRequirement.Type) + 1;

            return _requirementRepository.Create(mongoRequirement).ToDomainIdentity();
        }

        public Requirement ReadRequirement(Identity id)
        {
            var requirement = _requirementRepository.Read(id.ToMongoIdentity());
            if(requirement == null) { throw new EntityNotFoundException($"ID={id}");}

            var project = _projectRepository.Read(requirement.ProjectId);
            if(project == null) { throw new EntityNotFoundException($"ID={requirement.ProjectId}");}

            var user = _userRepository.Read(requirement.AuthorId);
            if (user == null)
            {
                //try to atleast get a project author, as author of the requirement
                var projectAuthor = _userRepository.Read(project.AuthorId);
                if (projectAuthor == null)
                {
                    throw new EntityNotFoundException($"ID={requirement.AuthorId}, {project.AuthorId}");
                }
            }

            return requirement.ToDomainEntity(user, project);
        }

        public bool UpdateRequirement(Requirement requirement)
        {
            var mongoRequirement = requirement.ToMongoEntity();

            if (_projectRepository.Count(_projectRepository.IdFilter(mongoRequirement.ProjectId)) == 0)
            {
                throw new EntityNotFoundException($"ID={requirement.Project.Id}");
            }

            if (_userRepository.Count(_userRepository.IdFilter(mongoRequirement.AuthorId)) == 0)
            {
                throw new EntityNotFoundException($"ID={requirement.Author.Id}");
            }

            var orderMarker = LastOrderMarker(mongoRequirement.ProjectId, requirement.Type.ToString()) + 1;

            var requirementFilter = _requirementRepository.IdFilter(mongoRequirement.Id);
            var updateDefinition = Builders<MongoRequirement>.Update
                .Set(x => x.AuthorId, mongoRequirement.AuthorId)
                .Set(x => x.Type, mongoRequirement.Type)
                .Set(x => x.Title, mongoRequirement.Title)
                .Set(x => x.Note, mongoRequirement.Note)
                .Set(x => x.OrderMarker, orderMarker);

            return _requirementRepository.Update(requirementFilter, updateDefinition);
        }

        public bool DeleteRequirement(Identity id)
        {
            return _requirementRepository.Delete(_requirementRepository.IdFilter(id.ToMongoIdentity()));
        }

        private int LastOrderMarker(ObjectId projectId, string type)
        {
            var projectFilter = Builders<MongoRequirement>.Filter.Eq(x => x.ProjectId, projectId);
            var typeFilter = Builders<MongoRequirement>.Filter.Eq(x => x.Type, type);
            var lastRequirement = _requirementRepository
                    .Find(projectFilter & typeFilter)
                    .OrderByDescending(x => x.OrderMarker)
                    .FirstOrDefault();

            return lastRequirement?.OrderMarker ?? -1;
        }
    }
}
