using System;
using System.Collections.Generic;
using System.Text;
using ReqTrack.Domain.Core.Entities.Users;
using ReqTrack.Persistence.Concrete.MongoDB.Entities;

namespace ReqTrack.Persistence.Concrete.MongoDB.Extensions.Mapping
{
    public static class UserMappingExtensions
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

        //TODO: extend with support for projects
        public static User ToDomainEntity(this MongoUser user)
        {
            return new User(
                user.Id.ToDomainIdentity(),
                user.Username,
                user.DisplayName,
                user.Password);
        }
    }
}
