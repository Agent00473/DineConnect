using Infrastructure.IntegrationEvents.Entities.Events;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.IntegrationEvents
{
    public interface ICreateIntegrationEventCommandHandler
    {
        Task<bool> AddHeartBeatEventDataAsync();
        Task AddIntegrationEventAsync(IEnumerable<IntegrationEvent> events, IDbContextTransaction transaction);
        Task AddIntegrationEventAsync(IntegrationEvent data, IDbContextTransaction transaction);
    }
}
