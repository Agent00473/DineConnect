
namespace Infrastructure.Domain.Response
{

    ///<summary>
    /// DTO class to send responses from API.
    ///</summary>
    ///<typeparam name="TValue">Holds the actual response data</typeparam>
    ///<typeparam name="TErrorCode">Defines the Error code for ErrorDetails class</typeparam>
    public class ResponseWrapper<TValue, TErrorCode> where TErrorCode : Enum
    {
        private bool _isSuccess;
        protected ResponseWrapper(TValue value)
        {
            _isSuccess = true;
            Value = value;
            Error = default;
        }
        protected ResponseWrapper(ErrorDetails<TErrorCode> error)
        {
            _isSuccess = false;
            Value = default;
            Error = error;
        }

        public readonly TValue? Value;
        public readonly ErrorDetails<TErrorCode>? Error;
        public bool IsSuccess => _isSuccess;
        public static implicit operator ResponseWrapper<TValue, TErrorCode>(TValue value) => new ResponseWrapper<TValue, TErrorCode>(value);
        public static implicit operator ResponseWrapper<TValue, TErrorCode>(ErrorDetails<TErrorCode> error) => new ResponseWrapper<TValue, TErrorCode>(error);

        public ResponseWrapper<TValue, TErrorCode> Match(Func<TValue, ResponseWrapper<TValue, TErrorCode>> success, Func<ErrorDetails<TErrorCode>, ResponseWrapper<TValue, TErrorCode>> failure)
        {
            if (_isSuccess)
            {
                return success(Value!);
            }
            return failure(Error!);
        }
    }

    public record ErrorDetails<TErrorCode>(TErrorCode Code, string Message) where TErrorCode : Enum
    {
        public static readonly ErrorDetails<TErrorCode> None = new(default, string.Empty);
    }


}
