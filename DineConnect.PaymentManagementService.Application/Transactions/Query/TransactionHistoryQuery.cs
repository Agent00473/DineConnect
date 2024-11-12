using DineConnect.PaymentManagementService.Application.Common;
using MediatR;

namespace DineConnect.PaymentManagementService.Application.Transactions.Query
{
    public record TransactionHistoryQuery(TransactionHistoryRequest Request): IRequest<PaymentResponseWrapper<TransactionHistoryResponse>>;

    /// <summary>
    /// Represents a request for retrieving a user's transaction history, filtered by date.
    /// </summary>
    public record TransactionHistoryRequest(
        Guid CustomerId,
        DateFilter Filter
    );

    public record DateFilter(DateTime StartDate, DateTime EndDate);

}
