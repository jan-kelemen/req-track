using System.Collections.Generic;
using System.Linq;
using ReqTrack.Domain.Core.Entities.Projects;
using ReqTrack.Domain.Core.Entities.Users;
using ReqTrack.Persistence.Concrete.MongoDB.Entities;

namespace ReqTrack.Persistence.Concrete.MongoDB.Extensions.Mapping
{
    internal static class UserMappingExtensions
    {
        public static MongoUser ToMongoEntity(this User user)
        {
            return new MongoUser
            {
                Id = user.Id.ToMongoIdentity(),
                DisplayName = user.DisplayName,
                Username = user.UserName,
                Password = user.PasswordHash,
            };
        }

        public static User ToDomainEntity(this MongoUser user, IEnumerable<MongoProject> projects = null)
        {
            var projs = projects?.Select(p => new BasicProject(p.Id.ToDomainIdentity(), p.Name));

            return new User(
                user.Id.ToDomainIdentity(),
                user.Username,
                user.DisplayName,
                user.Password,
                projs);
        }
    }
}
