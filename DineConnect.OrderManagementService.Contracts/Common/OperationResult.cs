using DineConnect.OrderManagementService.Domain.Common;

namespace DineConnect.OrderManagementService.Contracts.Common
{
    public class OperationResult<TValue, TErrorCode> where TErrorCode : Enum
    {
        public readonly TValue? Value;
        public readonly ErrorDetails<TErrorCode>? Error;

        private bool _isSuccess;

        private OperationResult(TValue value)
        {
            _isSuccess = true;
            Value = value;
            Error = default;
        }

        private OperationResult(ErrorDetails<TErrorCode> error)
        {
            _isSuccess = false;
            Value = default;
            Error = error;
        }

        public static implicit operator OperationResult<TValue, TErrorCode>(TValue value) => new OperationResult<TValue, TErrorCode>(value);

        public static implicit operator OperationResult<TValue, TErrorCode>(ErrorDetails<TErrorCode> error) => new OperationResult<TValue, TErrorCode>(error);

        public OperationResult<TValue, TErrorCode> Match(Func<TValue, OperationResult<TValue, TErrorCode>> success, Func<ErrorDetails<TErrorCode>, OperationResult<TValue, TErrorCode>> failure)
        {
            if (_isSuccess)
            {
                return success(Value!);
            }
            return failure(Error!);
        }
    }
}
