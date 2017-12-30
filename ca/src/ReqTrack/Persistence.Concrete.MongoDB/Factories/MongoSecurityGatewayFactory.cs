using MongoDB.Driver;
using ReqTrack.Domain.Core.Factories;
using ReqTrack.Domain.Core.Security;
using ReqTrack.Persistence.Concrete.MongoDB.Database;

namespace ReqTrack.Persistence.Concrete.MongoDB.Factories
{
    internal class MongoSecurityGatewayFactory : ISecurityGatewayFactory
    {
        private readonly MongoReqTrackDatabase _database;

        public MongoSecurityGatewayFactory(MongoReqTrackDatabase database)
        {
            _database = database;
        }

        public ISecurityGateway SecurityGateway { get; }
    }
}
