using DineConnect.PaymentManagementService.Application.Common;
using MediatR;

namespace DineConnect.PaymentManagementService.Application.Transactions.Query
{
    public class TransactionHistoryQueryHandler : IRequestHandler<TransactionHistoryQuery, PaymentResponseWrapper<TransactionHistoryResponse>>
    {
        public Task<PaymentResponseWrapper<TransactionHistoryResponse>> Handle(TransactionHistoryQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
