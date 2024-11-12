using RabbitMQ.Client.Events;

namespace Infrastructure.Messaging
{
    public interface IMessageSubscriber: IMessageServiceBase
    {
        void AddListener(EventHandler<BasicDeliverEventArgs> handler);
        void Configure(QueueConfiguration config);
        void RemoveListener(EventHandler<BasicDeliverEventArgs> handler);
    }
}