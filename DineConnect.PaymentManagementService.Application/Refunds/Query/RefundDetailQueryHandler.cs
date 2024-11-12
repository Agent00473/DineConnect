using DineConnect.PaymentManagementService.Application.Common;
using MediatR;

namespace DineConnect.PaymentManagementService.Application.Refunds.Query
{
    public class RefundDetailQueryHandler : IRequestHandler<RefundDetailQuery, PaymentResponseWrapper<RefundDetailsResponse>>
    {
        public Task<PaymentResponseWrapper<RefundDetailsResponse>> Handle(RefundDetailQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
