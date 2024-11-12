using DineConnect.PaymentManagementService.Application.Common;
using MediatR;

namespace DineConnect.PaymentManagementService.Application.Payments.Query
{
    public class PaymentDetailsQueryHandler : IRequestHandler<PaymentDetailsQuery, PaymentResponseWrapper<PaymentResponse>>
    {
        public Task<PaymentResponseWrapper<PaymentResponse>> Handle(PaymentDetailsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
