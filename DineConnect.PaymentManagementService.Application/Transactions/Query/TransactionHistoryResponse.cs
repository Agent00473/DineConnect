
using System.Transactions;

namespace DineConnect.PaymentManagementService.Application.Transactions.Query
{
    /// <summary>
    /// Response containing a user's transaction history, including transaction details.
    /// </summary>
    public record TransactionHistoryResponse(
        IList<TransactionRecord> Transactions,
        string Message
    );


    /// <summary>
    /// Represents an individual transaction record with details such as amount, date, and status.
    /// </summary>
    public record TransactionRecord(
        Guid TransactionId,
        decimal Amount,
        DateTime Date,
        int TransactionStatus,
        int PaymentMethod
    );
}
