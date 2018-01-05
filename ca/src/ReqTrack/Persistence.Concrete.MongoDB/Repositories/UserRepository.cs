using System.Linq;
using System.Threading;
using MongoDB.Bson;
using MongoDB.Driver;
using ReqTrack.Domain.Core.Entities;
using ReqTrack.Domain.Core.Entities.Users;
using ReqTrack.Domain.Core.Exceptions;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Persistence.Concrete.MongoDB.Database;
using ReqTrack.Persistence.Concrete.MongoDB.Entities;
using ReqTrack.Persistence.Concrete.MongoDB.Extensions.Mapping;

namespace ReqTrack.Persistence.Concrete.MongoDB.Repositories
{
    internal class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(MongoReqTrackDatabase database) : base(database)
        {
        }

        public Identity CreateUser(User user)
        {
            var userWithSameNameFilter = Builders<MongoUser>.Filter.Eq(x => x.Username, user.UserName);
            if (_userRepository.Count(userWithSameNameFilter) != 0)
            {
                //TODO: change this to something normal.
                throw new AbandonedMutexException();
            }

            return _userRepository.Create(user.ToMongoEntity()).ToDomainIdentity();
        }

        public User ReadUser(Identity id, bool loadProjects)
        {
            var mongoUser = _userRepository.Read(id.ToMongoIdentity());
            if(mongoUser == null) { throw new EntityNotFoundException($"ID={id}"); }

            var filter = Builders<MongoSecurityRights>.Filter.Eq(x => x.UserId, mongoUser.Id);
            var associatedProjects = _securityRightsRepository.Find(filter).Select(x => x.ProjectId);

            var projects = loadProjects ? _projectRepository.Read(associatedProjects) : null;

            return mongoUser.ToDomainEntity(projects);
        }

        public BasicUser ReadUserInfo(Identity id)
        {
            var mongoUser = _userRepository.Read(id.ToMongoIdentity());
            if (mongoUser == null) { throw new EntityNotFoundException($"ID={id}"); }

            return new BasicUser(mongoUser.Id.ToDomainIdentity(), mongoUser.DisplayName);
        }

        public BasicUser FindUserInfo(string username, string passwordHash)
        {
            var filter = Builders<MongoUser>.Filter.Where(x => x.Username == username && x.Password == passwordHash);
            var user = _userRepository.Find(filter).FirstOrDefault();
            if(user == null) { throw new EntityNotFoundException(); }

            return new BasicUser(user.Id.ToDomainIdentity(), user.DisplayName);
        }

        public bool UpdateUser(User user)
        {
            var filter = _userRepository.IdFilter(user.Id.ToMongoIdentity());
            if(_userRepository.Count(filter) == 0) { throw new EntityNotFoundException($"ID={user.Id}"); }

            var updateDefinition = Builders<MongoUser>.Update
                .Set(x => x.DisplayName, user.DisplayName)
                .Set(x => x.Password, user.PasswordHash);

            return _userRepository.Update(filter, updateDefinition);
        }

        public bool UpdateUserInfo(BasicUser user)
        {
            var filter = _userRepository.IdFilter(user.Id.ToMongoIdentity());
            if (_userRepository.Count(filter) == 0) { throw new EntityNotFoundException($"ID={user.Id}"); }

            var updateDefinition = Builders<MongoUser>.Update
                .Set(x => x.DisplayName, user.DisplayName);

            return _userRepository.Update(filter, updateDefinition);
        }

        public bool DeleteUser(Identity id)
        {
            var mongoIdentity = id.ToMongoIdentity();
            if (_userRepository.Count(_userRepository.IdFilter(mongoIdentity)) == 0)
            {
                throw new EntityNotFoundException($"ID={id}");
            }

            if (!_userRepository.Delete(_userRepository.IdFilter(mongoIdentity))) { return false; }

            var rightsFilter = Builders<MongoSecurityRights>.Filter.Eq(x => x.UserId, mongoIdentity);
            _securityRightsRepository.Delete(rightsFilter);

            //Delete projects where user is the author.
            var projectFilter = Builders<MongoProject>.Filter.Eq(x => x.AuthorId, mongoIdentity);
            var projectIds = _projectRepository.Find(projectFilter).Select(x => x.Id).ToList();
            _projectRepository.Delete(projectFilter);

            //Nullify references on requirements and use cases.
            var requirementUpdateFilter = Builders<MongoRequirement>.Filter.Eq(x => x.AuthorId, mongoIdentity);
            var requirementUpdateDefinition = Builders<MongoRequirement>.Update.Set(x => x.AuthorId, ObjectId.Empty);
            var requirementDeleteFilter = Builders<MongoRequirement>.Filter.Where(x => projectIds.Contains(x.Id));
            _requirementRepository.Update(requirementUpdateFilter, requirementUpdateDefinition);
            _requirementRepository.Delete(requirementDeleteFilter);

            var usecaseUpdateFilter = Builders<MongoUseCase>.Filter.Eq(x => x.AuthorId, mongoIdentity);
            var useCaseUpdateDefinition = Builders<MongoUseCase>.Update.Set(x => x.AuthorId, ObjectId.Empty);
            var useCaseDeleteFilter = Builders<MongoUseCase>.Filter.Where(x => projectIds.Contains(x.Id));
            _useCaseRepository.Update(usecaseUpdateFilter, useCaseUpdateDefinition);
            _useCaseRepository.Delete(useCaseDeleteFilter);

            return true;
        }


    }
}
