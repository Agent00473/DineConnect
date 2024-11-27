using DineConnect.PaymentManagementService.Application.Common;
using DineConnect.PaymentManagementService.Application.Common.Interfaces;
using MediatR;

namespace DineConnect.PaymentManagementService.Application.Payments.Query
{
    public class PaymentDetailsQueryHandler : IRequestHandler<PaymentDetailsQuery, PaymentResponseWrapper<PaymentResponse>>
    {
        private IPromotionService _service;
        public PaymentDetailsQueryHandler(IPromotionService service)
        {
            _service = service;
        }
        public Task<PaymentResponseWrapper<PaymentResponse>> Handle(PaymentDetailsQuery request, CancellationToken cancellationToken)
        {
            var discount = _service.GetDiscountDetails<PaymentRequest>(request.Data);
            PaymentResponse result = new PaymentResponse(request.Data.InvoiceId, request.Data.Amount, discount.DiscountAmount, 
                                                                request.Data.Tax, discount.FinalPrice, discount.Message);
            var wrapper =  PaymentResponseWrapper<PaymentResponse>.CreateSuccessResponse(result);
            return Task.FromResult(wrapper);

        }
    }
}
