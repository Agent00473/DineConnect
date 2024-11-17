
namespace DineConnect.OrderManagementService.Domain.Common
{

    public record ErrorDetails<TErrorCode>(TErrorCode Code, string Message) where TErrorCode : Enum
    {
        public static readonly ErrorDetails<TErrorCode> None = new(default, string.Empty);
    }
   
}
