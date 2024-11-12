using DineConnect.PaymentManagementService.Domain.Payment.Entities;

namespace DineConnect.PaymentManagementService.Infrastructure.DataAccess.Repositories
{
    public interface IRefundRepository
    {
        Task AddAsync(Refund refund);
        Task UpdateAsync(Refund refund);
    }
}