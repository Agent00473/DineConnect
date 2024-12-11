namespace Infrastructure.IntegrationEvents.Common
{
    public interface IIntegrationEventDataDispatcher
    {
        void Pause();
        void Start();
        void Stop();
        void AddData(Guid data);
    }
}