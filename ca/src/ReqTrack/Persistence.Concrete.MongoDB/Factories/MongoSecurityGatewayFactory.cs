using MongoDB.Driver;
using ReqTrack.Domain.Core.Factories;
using ReqTrack.Domain.Core.Security;
using ReqTrack.Persistence.Concrete.MongoDB.Database;
using ReqTrack.Persistence.Concrete.MongoDB.Security;

namespace ReqTrack.Persistence.Concrete.MongoDB.Factories
{
    internal class MongoSecurityGatewayFactory : ISecurityGatewayFactory
    {
        public MongoSecurityGatewayFactory(MongoReqTrackDatabase database)
        {
            SecurityGateway = new SecurityGateway(database);
        }

        public ISecurityGateway SecurityGateway { get; }
    }
}
