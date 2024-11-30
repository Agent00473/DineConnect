namespace Infrastructure.Messaging
{
   public interface IMessagePublisher: IMessageServiceBase
    {
        void Configure(QueueConfiguration config);
        bool SendMessage<TData>(string routingkey, EventMessage<TData> message);
    }
}