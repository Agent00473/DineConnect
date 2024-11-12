using DineConnect.PaymentManagementService.Application.Common;
using MediatR;

namespace DineConnect.PaymentManagementService.Application.Payments.Command
{
    public record ProcessPaymentCommand(
       IEnumerable<ProcessPaymentRequest> Data) : IRequest<PaymentResponseWrapper<PaymentStatusResponse>>;

    /// <summary>
    /// Represents a request to process a payment, containing payment method and identifiers.
    /// </summary>
    public record ProcessPaymentRequest(
        Guid PaymentId,
        int PaymentMethod,
        Guid CustomerId,
        Guid InvoiceId
    );
}
