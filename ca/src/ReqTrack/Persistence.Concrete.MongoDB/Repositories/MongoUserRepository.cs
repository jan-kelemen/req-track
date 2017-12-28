using System;
using System.Threading;
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

        public MongoUserRepository(IMongoCollection<MongoUser> users, IMongoCollection<MongoProject> projects)
        {
            _users = users;
            _projects = projects;
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
            var mongoUser = _users.Find(x => x.Id == id.ToMongoIdentity()).FirstOrDefault();

            if (mongoUser == null)
            {
                throw new AbandonedMutexException();
            }

            //TODO: handle projects

            return new ReadResult<User>(true, mongoUser.ToDomainEntity());
        }

        public ReadResult<BasicUser> ReadUserInfo(Identity id)
        {
            var projection = Builders<MongoUser>.Projection
                .Include(x => x.Id).Include(x => x.DisplayName);

            var mongoUser = _users
                .Find(x => x.Id == id.ToMongoIdentity())
                .Project<MongoUser>(projection)
                .FirstOrDefault();

            if (mongoUser == null)
            {
                throw new AbandonedMutexException();
            }

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

            //TODO: handle projects

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
            var filter = Builders<MongoUser>.Filter.Eq(x => x.Id, id.ToMongoIdentity());
            var mongoUser = _users.FindOneAndDelete(filter);
            return new DeleteResult<Identity>(true, mongoUser.Id.ToDomainIdentity());
        }
    }
}
