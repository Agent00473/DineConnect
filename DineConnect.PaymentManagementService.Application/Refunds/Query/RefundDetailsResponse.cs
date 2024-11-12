
namespace DineConnect.PaymentManagementService.Application.Refunds.Query
{
    /// <summary>
    /// Response containing refund details, including totals, discount, tax, and final refund amount.
    /// </summary>
    public record RefundDetailsResponse(
        Guid RefundId,
        decimal TotalAmount,
        decimal Discount,
        decimal Tax,
        decimal FinalRefundAmount,
        string Message
    );
}
