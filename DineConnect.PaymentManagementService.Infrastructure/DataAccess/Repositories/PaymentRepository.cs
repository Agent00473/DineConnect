using DineConnect.PaymentManagementService.Domain.Payment;
using DineConnect.PaymentManagementService.Domain.Payment.ValueObjects;

namespace DineConnect.PaymentManagementService.Infrastructure.DataAccess.Repositories
{
    public interface IPaymentRepository
    {
        Task AddAsync(Payment payment);
        Task<Payment> GetByIdAsync(PaymentId paymentId);
        Task UpdateAsync(Payment payment);
    }

    public class PaymentRepository : IPaymentRepository
    {
        private readonly DineOutPaymentDbContext _context;

        public PaymentRepository(DineOutPaymentDbContext context)
        {
            _context = context;
        }

        public async Task<Payment> GetByIdAsync(PaymentId paymentId)
        {
            return await _context.Payments.FindAsync(paymentId);
        }

        public async Task AddAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Payment payment)
        {
            _context.Payments.Update(payment);
            await _context.SaveChangesAsync();
        }
    }

}
