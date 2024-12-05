using Infrastructure.Messaging.Entities;

namespace Infrastructure.Messaging
{
    public interface IQueueMessagePublisher: IMessageServiceBase
    {
        bool SendMessage(string routingkey, EventMessage message);
    }
}