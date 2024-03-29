﻿using ReqTrack.Domain.Core.Factories;
using ReqTrack.Persistence.Concrete.MongoDB.Database;
using ReqTrack.Persistence.Concrete.MongoDB.Factories;

namespace ReqTrack.Persistence.Concrete.MongoDB.Initialization
{
    public class Initializer
    {
        public Initializer(string connectionString = "mongodb://localhost:27017", string databaseName = "ReqTrack")
        {
            var database = new MongoReqTrackDatabase(connectionString, databaseName);
            RepositoryFactory = new MongoRepositoryFactory(database);
            SecurityGatewayFactory = new MongoSecurityGatewayFactory(database);
        }

        public IRepositoryFactory RepositoryFactory { get; }

        public ISecurityGatewayFactory SecurityGatewayFactory { get; }
    }
}
