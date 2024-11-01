
namespace DineConnect.OrderManagementService.Domain.Common
{
    public enum ErrorType
    {
        None = 0,
        NotFound = 1,
        Validation = 2,
        Conflict = 3,
        AccessUnAuthorized = 4,
        AccessForbidden = 5,
        ErrorType = 6
    }

 
    public enum OrderErrorCode
    {
        None = 0,
        InvalidOrderId = 1,
        OrderNotFound = 2,
        PaymentFailed = 3
    }

    public record ErrorDetails<TErrorCode>(ErrorType Type, TErrorCode Code, string Message) where TErrorCode : Enum
    {
        public static readonly ErrorDetails<TErrorCode> None = new(ErrorType.None, default, string.Empty);
    }
   
}
