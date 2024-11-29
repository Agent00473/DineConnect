namespace Infrastructure.Messaging
{
    public interface IMessagePublisher<TData> : IMessageServiceBase
    {
        void Configure(QueueConfiguration config);
        bool SendMessage(string routingkey, EventMessage<TData> message);
    }
}