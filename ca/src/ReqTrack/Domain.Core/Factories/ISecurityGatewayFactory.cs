using ReqTrack.Domain.Core.Security;

namespace ReqTrack.Domain.Core.Factories
{
    public interface ISecurityGatewayFactory
    {
        ISecurityGateway SecurityGateway { get; }
    }
}
