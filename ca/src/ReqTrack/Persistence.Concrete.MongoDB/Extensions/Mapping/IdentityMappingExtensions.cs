﻿using MongoDB.Bson;
using ReqTrack.Domain.Core.Entities;

namespace ReqTrack.Persistence.Concrete.MongoDB.Extensions.Mapping
{
    internal static class IdentityMappingExtensions
    {
        public static ObjectId ToMongoIdentity(this Identity id) => ObjectId.Parse(id);

        public static Identity ToDomainIdentity(this ObjectId id) => Identity.FromString(id.ToString());
    }
}
