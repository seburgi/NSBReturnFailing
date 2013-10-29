using NServiceBus;

namespace NsbReturnFailing.Endpoint
{
    public class EndpointConfig : IConfigureThisEndpoint, AsA_Publisher, UsingTransport<SqlServer> {}
}