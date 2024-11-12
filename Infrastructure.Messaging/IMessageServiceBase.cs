namespace Infrastructure.Messaging
{
    public interface IMessageServiceBase
    {
        void Configure(QueueConfiguration config);
        void Dispose();
    }
}