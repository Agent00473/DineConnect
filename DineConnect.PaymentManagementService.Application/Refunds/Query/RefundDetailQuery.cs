using DineConnect.PaymentManagementService.Application.Common;
using MediatR;

namespace DineConnect.PaymentManagementService.Application.Refunds.Query
{
    public record RefundDetailQuery(RefundDetailRequest Request) : IRequest<PaymentResponseWrapper<RefundDetailsResponse>>;

    /// <summary>
    /// Represents a refund request, including refund amount and reason.
    /// </summary>
    public record RefundDetailRequest(
        Guid CustomerId,
        Guid InvoiceId,
        Guid PaymentId,
        decimal RefundAmount,
        int RefundReason
    );

}
