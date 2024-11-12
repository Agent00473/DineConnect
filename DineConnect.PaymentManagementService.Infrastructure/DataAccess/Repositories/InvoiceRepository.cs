using DineConnect.PaymentManagementService.Domain.Invoice.ValueObjects;
using DineConnect.PaymentManagementService.Domain.Invoice;

namespace DineConnect.PaymentManagementService.Infrastructure.DataAccess.Repositories
{
    public interface IInvoiceRepository
    {
        Task AddAsync(Invoice invoice);
        Task<Invoice> GetByIdAsync(InvoiceId invoiceId);
    }

    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly DineOutPaymentDbContext _context;

        public InvoiceRepository(DineOutPaymentDbContext context)
        {
            _context = context;
        }

        public async Task<Invoice> GetByIdAsync(InvoiceId invoiceId)
        {
            return await _context.Invoices.FindAsync(invoiceId);
        }

        public async Task AddAsync(Invoice invoice)
        {
            await _context.Invoices.AddAsync(invoice);
            await _context.SaveChangesAsync();
        }
    }

}
