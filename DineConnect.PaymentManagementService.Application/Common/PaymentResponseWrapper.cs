namespace DineConnect.PaymentManagementService.Application.Common
{
    public class PaymentResponseWrapper<TResponse> where TResponse : class
    {
        private bool _isSuccess;

        protected PaymentResponseWrapper(TResponse data)
        {
            _isSuccess = true;
            Data = data;
            Error = default;
        }

        protected PaymentResponseWrapper(ErrorDetails error)
        {
            _isSuccess = false;
            Data = default;
            Error = error;
        }

        public bool IsSuccess => _isSuccess;
        public readonly TResponse Data;
        public readonly ErrorDetails? Error;

        public static PaymentResponseWrapper<TResponse> CreateSuccessResponse(TResponse data)
        {
            return new PaymentResponseWrapper<TResponse>(data);
        }
        public static PaymentResponseWrapper<TResponse> CreateErrorResponse(ErrorDetails error)
        {
            return new PaymentResponseWrapper<TResponse>(error);
        }
    }

    public enum ErrorType
    {
        None = 0,
        NotFound = 1,
        Validation = 2,
        Conflict = 3,
        Authorization = 4,
        AccessForbidden = 5,
    }


    public record ErrorDetails(ErrorType Type, string Message) 
    {
        public static readonly ErrorDetails None = new(ErrorType.None,  string.Empty);
    }
}
