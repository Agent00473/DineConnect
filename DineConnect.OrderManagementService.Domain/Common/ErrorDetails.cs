
namespace DineConnect.OrderManagementService.Domain.Common
{
    public enum ErrorType
    {
        None = 0,
        NotFound = 1,
        Validation = 2,
        Conflict = 3,
        Authorization = 4,
        AccessForbidden = 5,
    }


    public record ErrorDetails<TErrorCode>(ErrorType Type, TErrorCode Code, string Message) where TErrorCode : Enum
    {
        public static readonly ErrorDetails<TErrorCode> None = new(ErrorType.None, default, string.Empty);
    }
   
}
