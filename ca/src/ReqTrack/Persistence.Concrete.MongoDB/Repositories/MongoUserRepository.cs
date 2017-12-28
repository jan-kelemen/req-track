using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using MongoDB.Bson;
using MongoDB.Driver;
using ReqTrack.Domain.Core.Entities;
using ReqTrack.Domain.Core.Entities.Users;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.Core.Repositories.Results;
using ReqTrack.Persistence.Concrete.MongoDB.Entities;
using ReqTrack.Persistence.Concrete.MongoDB.Extensions.Mapping;

namespace ReqTrack.Persistence.Concrete.MongoDB.Repositories
{
    public class MongoUserRepository : MongoBaseRepository, IUserRepository
    {
        private readonly IMongoCollection<MongoUser> _users;

        private readonly IMongoCollection<MongoProject> _projects;

        private readonly IMongoCollection<MongoRequirement> _requirements;

        private readonly IMongoCollection<MongoUseCase> _useCases;

        public MongoUserRepository(
            IMongoCollection<MongoUser> users, 
            IMongoCollection<MongoProject> projects,
            IMongoCollection<MongoRequirement> requirements,
            IMongoCollection<MongoUseCase> useCases)
        {
            _users = users;
            _projects = projects;
            _requirements = requirements;
            _useCases = useCases;
        }

        public CreateResult<User> CreateUser(User user)
        {
            var filter = Builders<MongoUser>.Filter.Eq(x => x.Username, user.UserName);
            if (_users.Count(filter) != 0)
            {
                //TODO: change this to something normal.
                throw new AbandonedMutexException();
            }

            var mongoUser = user.ToMongoEntity();
            _users.InsertOne(mongoUser);

            return new CreateResult<User>(true, mongoUser.ToDomainEntity());
        }

        public ReadResult<User> ReadUser(Identity id, bool loadProjects)
        {
            var mongoUser = FindByIdOrThrow(_users, id.ToMongoIdentity());
            var projects = _projects.Find(x => mongoUser.AssociatedProjects.Contains(x.Id)).ToEnumerable();
            return new ReadResult<User>(true, mongoUser.ToDomainEntity(projects));
        }

        public ReadResult<BasicUser> ReadUserInfo(Identity id)
        {
            var mongoUser = FindByIdOrThrow(_users, id.ToMongoIdentity());

            return new ReadResult<BasicUser>(
                true, 
                new BasicUser(mongoUser.Id.ToDomainIdentity(), mongoUser.DisplayName));
        }

        public UpdateResult<User> UpdateUser(User user, bool updateProjects)
        {
            var filter = Builders<MongoUser>.Filter.Eq(x => x.Id, user.Id.ToMongoIdentity());

            var updateDefinition = Builders<MongoUser>.Update
                .Set(x => x.DisplayName, user.DisplayName)
                .Set(x => x.Password, user.PasswordHash);

            if (updateProjects)
            {
                var projects = user.Projects.Select(x => x.Id.ToMongoIdentity());
                updateDefinition.Set(x => x.AssociatedProjects, projects);
            }

            var options = new FindOneAndUpdateOptions<MongoUser> {ReturnDocument = ReturnDocument.After};

            var mongoUser = _users.FindOneAndUpdate(filter, updateDefinition, options);

            return new UpdateResult<User>(true, mongoUser.ToDomainEntity());
        }

        public UpdateResult<BasicUser> UpdateUserInfo(BasicUser user)
        {
            var projection = Builders<MongoUser>.Projection
                .Include(x => x.Id).Include(x => x.DisplayName);

            var filter = Builders<MongoUser>.Filter.Eq(x => x.Id, user.Id.ToMongoIdentity());

            var updateDefinition = Builders<MongoUser>.Update
                .Set(x => x.DisplayName, user.DisplayName);

            var options = new FindOneAndUpdateOptions<MongoUser>
            {
                ReturnDocument = ReturnDocument.After,
                Projection = projection,
            };

            var mongoUser = _users.FindOneAndUpdate(filter, updateDefinition, options);

            return new UpdateResult<BasicUser>(true, mongoUser.ToDomainEntity());
        }

        public DeleteResult<Identity> DeleteUser(Identity id)
        {
            var mongoIdentity = id.ToMongoIdentity();

            var projectFilter = Builders<MongoProject>.Filter.Eq(x => x.AuthorId, mongoIdentity);
            var projectUpdateDefinition = Builders<MongoProject>.Update.Set(x => x.AuthorId, ObjectId.Empty);
            _projects.UpdateMany(projectFilter, projectUpdateDefinition);

            var requirementFilter = Builders<MongoRequirement>.Filter.Eq(x => x.AuthorId, mongoIdentity);
            var requirementUpdateDefinition = Builders<MongoRequirement>.Update.Set(x => x.AuthorId, ObjectId.Empty);
            _requirements.UpdateMany(requirementFilter, requirementUpdateDefinition);

            var useCaseFilter = Builders<MongoUseCase>.Filter.Eq(x => x.AuthorId, mongoIdentity);
            var useCaseUpdateDefinition = Builders<MongoUseCase>.Update.Set(x => x.AuthorId, ObjectId.Empty);
            _useCases.UpdateMany(useCaseFilter, useCaseUpdateDefinition);

            var userFilter = Builders<MongoUser>.Filter.Eq(x => x.Id, mongoIdentity);
            var mongoUser = _users.FindOneAndDelete(userFilter);

            return new DeleteResult<Identity>(true, mongoUser.Id.ToDomainIdentity());
        }
    }
}
