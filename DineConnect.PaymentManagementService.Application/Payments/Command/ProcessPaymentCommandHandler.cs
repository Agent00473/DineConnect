using DineConnect.PaymentManagementService.Application.Common;
using MediatR;

namespace DineConnect.PaymentManagementService.Application.Payments.Command
{
    public class ProcessPaymentCommandHandler : IRequestHandler<ProcessPaymentCommand, PaymentResponseWrapper<PaymentStatusResponse>>
    {
        public Task<PaymentResponseWrapper<PaymentStatusResponse>> Handle(ProcessPaymentCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
