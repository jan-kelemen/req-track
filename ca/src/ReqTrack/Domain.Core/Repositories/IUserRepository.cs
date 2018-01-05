using ReqTrack.Domain.Core.Entities;
using ReqTrack.Domain.Core.Entities.Users;

namespace ReqTrack.Domain.Core.Repositories
{
    public interface IUserRepository : IRepository
    {
        Identity CreateUser(User user);

        User ReadUser(Identity id, bool loadProjects);

        BasicUser ReadUserInfo(Identity id);

        BasicUser FindUserInfo(string username, string passwordHash);

        bool UpdateUser(User user);

        bool UpdateUserInfo(BasicUser user);

        bool DeleteUser(Identity id);
    }
}
