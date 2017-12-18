using ReqTrack.Domain.Core.Repositories;
using System.Linq;
using ReqTrack.Domain.Core.Entities;
using ReqTrack.Domain.Core.Entities.Requirements;
using ReqTrack.Domain.Core.Repositories.Results;
using ReqTrack.Persistence.Concrete.MongoDB.Database;
using ReqTrack.Persistence.Concrete.MongoDB.Entities;
using MongoDB.Driver;
using ReqTrack.Persistence.Concrete.MongoDB.Entities.Extensions;

namespace ReqTrack.Persistence.Concrete.MongoDB.Repositories
{
    internal class MongoRequirementRepository : AbstractRepository, IRequirementRepository
    {
        private MongoReqTrackDatabase _database;

        public MongoRequirementRepository(MongoReqTrackDatabase database)
        {
            _database = database;
        }

        private IMongoCollection<MongoProject> _projectCollection => _database.ProjectCollection;

        private IMongoCollection<MongoRequirement> _requirementCollection => _database.RequirementCollection;

        public CreateResult<Requirement> CreateRequirement(Requirement requirement)
        {
            var entity = requirement.ToMongoEntity();
            _requirementCollection.InsertOne(entity);
            var created = _requirementCollection.Find(p => p.Id == entity.Id).FirstOrDefault();
            //TODO: handle null?
            var domainEntity = created.ToDomainEntity();

            //Add requirement to the project.
            var filter = Builders<MongoProject>.Filter.Eq(x => x.Id, created.ProjectId);
            var updateDefinition = Builders<MongoProject>.Update.Push(x => x.RequirementIds, created.Id);
            var result = _projectCollection.FindOneAndUpdate(filter, updateDefinition);

            return new CreateResult<Requirement>(true, domainEntity);
        }

        public ReadResult<Requirement> ReadRequirement(Identity id)
        {
            var entity = _requirementCollection.Find(r => r.Id == id.ToMongoIdentity()).FirstOrDefault();
            //TODO: handle null?
            var domainEntity = entity.ToDomainEntity();
            return new ReadResult<Requirement>(true, domainEntity);
        }

        public UpdateResult<Requirement> UpdateRequirement(Requirement requirement)
        {
            var entity = requirement.ToMongoEntity();

            //TODO: handle if project id has changed -- invalid request

            var filter = Builders<MongoRequirement>.Filter.Eq(x => x.Id, entity.Id);
            var updateDefinition = Builders<MongoRequirement>.Update
                .Set(x => x.Title, entity.Title)
                .Set(x => x.Type, entity.Type)
                .Set(x => x.Details, entity.Details);
            var options = new FindOneAndUpdateOptions<MongoRequirement>
            {
                ReturnDocument = ReturnDocument.After,
            };
            var updated = _requirementCollection.FindOneAndUpdate(filter, updateDefinition, options);

            var domainEntity = updated.ToDomainEntity();

            return new UpdateResult<Requirement>(true, domainEntity);
        }

        public DeleteResult<Identity> DeleteRequirement(Identity id)
        {
            var result = _requirementCollection.FindOneAndDelete(p => p.Id == id.ToMongoIdentity());

            //Remove requirement from the project
            var filter = Builders<MongoProject>.Filter.Eq(x => x.Id, result.ProjectId);
            var updateDefinition = Builders<MongoProject>.Update
                .Pull(x => x.RequirementIds, result.Id);
            _projectCollection.UpdateOne(filter, updateDefinition);

            return new DeleteResult<Identity>(true, id);
        }

        public DeleteResult<Identity> DeleteRequirement(Requirement requirement)
        {
            return DeleteRequirement(requirement.Id);
        }
    }
}
