using DineConnect.PaymentManagementService.Domain.Payment.Entities;

namespace DineConnect.PaymentManagementService.Infrastructure.DataAccess.Repositories
{
    public class RefundRepository : IRefundRepository
    {
        private readonly DineOutPaymentDbContext _context;
        public RefundRepository(DineOutPaymentDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Refund refund)
        {
            await _context.Refunds.AddAsync(refund);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Refund refund)
        {
            _context.Refunds.Update(refund);
            await _context.SaveChangesAsync();
        }
    }



}
