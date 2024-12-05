using Infrastructure.Messaging.Entities;

namespace Infrastructure.Messaging
{
    public interface IMessagePublisher: IMessageServiceBase
    {
        bool SendMessage(string routingkey, EventMessage message);
    }
}