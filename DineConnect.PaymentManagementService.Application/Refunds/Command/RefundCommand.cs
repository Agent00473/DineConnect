using DineConnect.PaymentManagementService.Application.Common;
using MediatR;

namespace DineConnect.PaymentManagementService.Application.Refunds.Command
{
    public record RefundCommand(ProcessRefundRequest Data) : IRequest<PaymentResponseWrapper<RefundStatusResponse>>;
    
    /// <summary>
    /// Represents a request to process a refund, with refund ID and specific refund details.
    /// </summary>
    public record ProcessRefundRequest(
        Guid RefundId,
        Guid PaymentId,
        int RefundReason,
        decimal RefundAmount
    );
}
