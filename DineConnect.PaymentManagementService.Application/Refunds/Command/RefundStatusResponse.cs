
namespace DineConnect.PaymentManagementService.Application.Refunds.Command
{
    /// <summary>
    /// Response containing the status and details of a processed refund.
    /// </summary>
    public record RefundStatusResponse(
        Guid RefundId,
        int RefundStatus,
        DateTime ProcessedDate,
        string Message
    );
}
