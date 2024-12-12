using Infrastructure.IntegrationEvents.Entities.Events;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.IntegrationEvents
{
    public interface IAddIntegrationEventCommandHandler
    {
        Task<bool> AddHeartBeatEventDataAsync();
        Task AddIntegrationEventAsync(IEnumerable<IntegrationEvent> events);
        Task AddIntegrationEventAsync(IntegrationEvent data, IDbContextTransaction transaction);
        Task AddIntegrationEventAsync(IntegrationEvent data, Guid transactionID);
        Task AddIntegrationEventAsync(IntegrationEvent data);
    }

}
