﻿using MongoDB.Bson;
using MongoDB.Driver;
using ReqTrack.Domain.Core.Entities;
using ReqTrack.Domain.Core.Entities.Requirements;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Persistence.Concrete.MongoDB.Entities;
using ReqTrack.Persistence.Concrete.MongoDB.Extensions.Mapping;

namespace ReqTrack.Persistence.Concrete.MongoDB.Repositories.Internal
{
    internal class MongoRequirementRepository : MongoBaseRepository, IRequirementRepository
    {
        private readonly IMongoCollection<MongoRequirement> _requirements;

        private readonly IMongoCollection<MongoProject> _projects;

        private readonly IMongoCollection<MongoUser> _users;

        public MongoRequirementRepository(
            IMongoCollection<MongoRequirement> requirements,
            IMongoCollection<MongoProject> projects,
            IMongoCollection<MongoUser> users)
        {
            _requirements = requirements;
            _projects = projects;
            _users = users;
        }

        public Identity CreateRequirement(Requirement requirement)
        {
            var mongoRequirement = requirement.ToMongoEntity();

            var project = FindByIdOrThrow(_projects, mongoRequirement.ProjectId);
            var user = FindByIdOrThrow(_users, mongoRequirement.AuthorId);

            mongoRequirement.OrderMarker = LastOrderMarker(project.Id, mongoRequirement.Type) + 1;

            _requirements.InsertOne(mongoRequirement);

            return mongoRequirement.Id.ToDomainIdentity();
        }

        public Requirement ReadRequirement(Identity id)
        {
            var mongoRequirement = FindByIdOrThrow(_requirements, id.ToMongoIdentity());
            var mongoProject = FindByIdOrThrow(_projects, mongoRequirement.ProjectId);
            var mongoUser = FindByIdOrThrow(_users, mongoRequirement.AuthorId);

            return mongoRequirement.ToDomainEntity(mongoUser, mongoProject);
        }

        public bool UpdateRequirement(Requirement requirement)
        {
            var mongoRequirement = requirement.ToMongoEntity();

            var mongoProject = FindByIdOrThrow(_projects, mongoRequirement.ProjectId);
            var mongoUser = FindByIdOrThrow(_users, mongoRequirement.AuthorId);
            var orderMarker = LastOrderMarker(mongoProject.Id, requirement.Type.ToString()) + 1;

            var requirementFilter = Builders<MongoRequirement>.Filter.Eq(x => x.Id, mongoRequirement.Id);
            var updateDefinition = Builders<MongoRequirement>.Update
                .Set(x => x.AuthorId, mongoUser.Id)
                .Set(x => x.Type, mongoRequirement.Type)
                .Set(x => x.Title, mongoRequirement.Title)
                .Set(x => x.Note, mongoRequirement.Note)
                .Set(x => x.OrderMarker, orderMarker);
            var options = new FindOneAndUpdateOptions<MongoRequirement>
            {
                ReturnDocument = ReturnDocument.After,
            };

            var updated = _requirements.FindOneAndUpdate(requirementFilter, updateDefinition, options);

            return true;
        }

        public bool DeleteRequirement(Identity id)
        {
            var filter = Builders<MongoRequirement>.Filter.Eq(x => x.Id, id.ToMongoIdentity());
            var entity = _requirements.FindOneAndDelete(filter);
            return true;
        }

        private int LastOrderMarker(ObjectId projectId, string type)
        {
            var projectFilter = Builders<MongoRequirement>.Filter.Eq(x => x.ProjectId, projectId);
            var typeFilter = Builders<MongoRequirement>.Filter.Eq(x => x.Type, type);
            var lastRequirement = _requirements
                .Find(projectFilter & typeFilter)
                .SortByDescending(x => x.OrderMarker) //https://stackoverflow.com/a/41503983 perfectly fine
                .Limit(1)
                .FirstOrDefault();

            return lastRequirement?.OrderMarker ?? -1;
        }
    }
}