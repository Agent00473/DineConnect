using DineConnect.PaymentManagementService.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DineConnect.PaymentManagementService.Application.Refunds.Command
{
    internal class RefundCommandHandler : IRequestHandler<RefundCommand, PaymentResponseWrapper<RefundStatusResponse>>
    {
        public Task<PaymentResponseWrapper<RefundStatusResponse>> Handle(RefundCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
