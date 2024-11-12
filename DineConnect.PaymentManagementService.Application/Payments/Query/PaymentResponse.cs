using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DineConnect.PaymentManagementService.Application.Payments.Query
{
    /// <summary>
    /// Response containing payment details, including totals, discount, tax, and final invoice amount.
    /// </summary>
    public record PaymentResponse(
        Guid InvoiceId,
        decimal TotalAmount,
        decimal Discount,
        decimal Tax,
        decimal FinalInvoiceAmount,
        string Message
    );
}
