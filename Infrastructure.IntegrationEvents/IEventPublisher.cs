
namespace Infrastructure.IntegrationEvents
{
    public interface IEventPublisher
    {
        public Task<bool> Publish(Guid transactionId);
    }
}
