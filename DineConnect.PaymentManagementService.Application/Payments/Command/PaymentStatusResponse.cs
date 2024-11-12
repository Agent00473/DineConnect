using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DineConnect.PaymentManagementService.Application.Payments.Command
{
    /// <summary>
    /// Response containing the status and details of a processed payment.
    /// </summary>
    public record PaymentStatusResponse(
        Guid PaymentId,
        int PaymentStatus,
        DateTime ProcessedDate,
        string Message
    );
}
