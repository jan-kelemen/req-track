using ReqTrack.Domain.Core.Repositories;
using System.Collections.Generic;
using ReqTrack.Domain.Core.Entities;
using ReqTrack.Domain.Core.Entities.Project;
using ReqTrack.Domain.Core.Repositories.Results;
using ReqTrack.Persistence.Concrete.MongoDB.Database;
using ReqTrack.Persistence.Concrete.MongoDB.Entities;
using MongoDB.Driver;
using ReqTrack.Persistence.Concrete.MongoDB.Entities.Extensions;
using System.Linq;
using MongoDB.Bson;

namespace ReqTrack.Persistence.Concrete.MongoDB.Repositories
{
    internal class MongoProjectRepository : IProjectRepository
    {
        private MongoReqTrackDatabase _database;

        internal MongoProjectRepository(MongoReqTrackDatabase database)
        {
            _database = database;
        }

        private IMongoCollection<Project> _projectCollection => _database.ProjectCollection;

        public CreateResult<ProjectInfo> CreateProject(ProjectInfo projectInfo)
        {
            var entity = projectInfo.ToMongoEntity();
            _projectCollection.InsertOne(entity);
            var created = _projectCollection.Find(p => p.Id == entity.Id).FirstOrDefault();
            //TODO: handle null?
            var domainEntity = created.ToDomainEntity();

            return new CreateResult<ProjectInfo>(true, domainEntity);
        }

        public ReadResult<IEnumerable<ProjectInfo>> ReadAllProjects()
        {
            var entities = _projectCollection.Find(_ => true).ToList().Select(p => p.ToDomainEntity());
            return new ReadResult<IEnumerable<ProjectInfo>>(true, entities);
        }

        public ReadResult<ProjectInfo> ReadProject(Identity id)
        {
            var entity = _projectCollection.Find(p => p.Id == ObjectId.Parse(id.ToString())).FirstOrDefault();
            var domainEntity = entity.ToDomainEntity();
            //TODO: handle null
            return new ReadResult<ProjectInfo>(true, domainEntity);
        }

        public UpdateResult<ProjectInfo> UpdateProject(ProjectInfo projectInfo)
        {
            var entity = projectInfo.ToMongoEntity();
            var updateDefinition = Builders<Project>.Update.Set(p => p.Name, projectInfo.Name);
            var result = _projectCollection.UpdateOne(p => p.Id == entity.Id, updateDefinition);
            var readResult = ReadProject(projectInfo.Id);
            return new UpdateResult<ProjectInfo>(result.ModifiedCount == 1 && readResult, readResult.Read);
        }

        public DeleteResult<Identity> DeleteProject(Identity id)
        {
            var result = _projectCollection.DeleteOne(p => p.Id == ObjectId.Parse(id.ToString()));
            return new DeleteResult<Identity>(result.DeletedCount == 1, id);
        }

        public DeleteResult<Identity> DeleteProject(ProjectInfo projectInfo)
        {
            return DeleteProject(projectInfo.Id);
        }
    }
}
