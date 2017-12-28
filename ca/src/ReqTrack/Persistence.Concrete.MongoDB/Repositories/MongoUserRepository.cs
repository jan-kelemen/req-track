using System;
using ReqTrack.Domain.Core.Entities;
using ReqTrack.Domain.Core.Entities.Users;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.Core.Repositories.Results;

namespace ReqTrack.Persistence.Concrete.MongoDB.Repositories
{
    public class MongoUserRepository : MongoBaseRepository, IUserRepository
    {
        public CreateResult<User> CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public ReadResult<User> ReadUser(Identity id, bool loadProjects)
        {
            throw new NotImplementedException();
        }

        public ReadResult<BasicUser> ReadUserInfo(Identity id)
        {
            throw new NotImplementedException();
        }

        public UpdateResult<User> UpdateUser(User user, bool updateProjects)
        {
            throw new NotImplementedException();
        }

        public UpdateResult<BasicUser> UpdateUserInfo(BasicUser user)
        {
            throw new NotImplementedException();
        }

        public DeleteResult<Identity> DeleteUser(Identity id)
        {
            throw new NotImplementedException();
        }
    }
}
