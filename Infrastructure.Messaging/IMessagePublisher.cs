using Infrastructure.Messaging.Entities;
using Infrastructure.Messaging.Implementation.RabbitMQ;
using Infrastructure.Messaging.Implementation.RabbitMQ.Configs;

namespace Infrastructure.Messaging
{
    public interface IMessagePublisher: IMessageServiceBase
    {
        bool SendMessage(RouteData route, EventMessage message);
    }
}