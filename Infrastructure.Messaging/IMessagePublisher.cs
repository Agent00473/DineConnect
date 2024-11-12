namespace Infrastructure.Messaging
{
    public interface IMessagePublisher<TData> : IMessageServiceBase
    {
        void Configure(QueueConfiguration config);
        void SendMessage(string routingkey, EventMessage<TData> message);
    }
}