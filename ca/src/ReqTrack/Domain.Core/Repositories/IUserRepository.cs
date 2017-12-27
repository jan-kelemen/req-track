using ReqTrack.Domain.Core.Entities;
using ReqTrack.Domain.Core.Entities.Users;
using ReqTrack.Domain.Core.Repositories.Results;

namespace ReqTrack.Domain.Core.Repositories
{
    public interface IUserRepository : IRepository
    {
        CreateResult<User> CreateUser(User user);

        ReadResult<User> ReadUser(Identity id, bool loadProjects);

        ReadResult<BasicUser> ReadUserInfo(Identity id);

        UpdateResult<User> UpdateUser(User user, bool updateProjects);

        UpdateResult<BasicUser> UpdateUserInfo(BasicUser user);

        DeleteResult<Identity> DeleteUser(Identity id);
    }
}
