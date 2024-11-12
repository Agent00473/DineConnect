
using DineConnect.PaymentManagementService.Application.Common;
using MediatR;

namespace DineConnect.PaymentManagementService.Application.Payments.Query
{
    public record PaymentDetailsQuery(PaymentRequest Data) : IRequest<PaymentResponseWrapper<PaymentResponse>>
    {

    }
    /// <summary>
    /// Represents a request for initial payment details, including invoice and amount information.
    /// </summary>
    public record PaymentRequest(
        Guid CustomerId,
        Guid InvoiceId,
        decimal Amount,
        string Currency,
        decimal Discount,
        decimal Tax
    );
}
