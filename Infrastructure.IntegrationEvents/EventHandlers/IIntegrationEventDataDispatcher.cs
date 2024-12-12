namespace Infrastructure.IntegrationEvents.EventHandlers
{
    public interface IIntegrationEventDataDispatcher
    {
        void Pause();
        void Start();
        void Stop();
        void AddData(Guid data);
    }
}