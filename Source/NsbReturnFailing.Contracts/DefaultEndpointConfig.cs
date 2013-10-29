using System;
using NServiceBus;
using NServiceBus.Features;

namespace NsbReturnFailing.Contracts
{
    public interface IMessage { }
    public interface ICommand : IMessage { }
    public interface IEvent : IMessage { }

    public class MyCommand : ICommand
    {
        public int Id { get; set; }
        public string Data { get; set; }
    }

    public class MyEvent : IEvent
    {
        public int Id { get; set; }
        public string Data { get; set; }
    }
    
    public class DefaultEndpointConfig : IWantToRunBeforeConfiguration
    {
        public void Init()
        {
            Configure.With().Log4Net()
                     .DefiningCommandsAs(type => !type.IsAbstract && !type.IsInterface && typeof(ICommand).IsAssignableFrom(type))
                     .DefiningEventsAs(type => !type.IsAbstract && !type.IsInterface && typeof(IEvent).IsAssignableFrom(type))
                     .DefiningMessagesAs(type => !type.IsAbstract && !type.IsInterface && typeof(IMessage).IsAssignableFrom(type));

            Configure.Serialization.Json();
            Configure.Features.Enable<StorageDrivenPublisher>();
            Configure.Transactions.Enable();
            Configure.Transactions.Advanced(settings => settings.DisableDistributedTransactions());
        }
    }
}