using DineConnect.OrderManagementService.Domain.Common;

namespace DineConnect.OrderManagementService.Domain.Customers
{
    public enum CustomerErrorCode
    {
        None = 0,
        InvalidCustomerId = 1,
        CustomerNotFound = 2,
        DuplicateCustomer = 3,
        EmailAlreadyRegistered = 4,
        InvalidAddress = 5,
        InvalidName = 6,
        InvalidEmail = 7,

    }

    public sealed record CustomerErrorDetails
    {
        public static readonly ErrorDetails<CustomerErrorCode> None = new(ErrorType.None, CustomerErrorCode.None, string.Empty);
        public static readonly ErrorDetails<CustomerErrorCode> InvalidCustomerId = new(ErrorType.Validation, CustomerErrorCode.InvalidCustomerId, "Customer ID is not valid.");
        public static readonly ErrorDetails<CustomerErrorCode> CustomerNotFound = new(ErrorType.NotFound, CustomerErrorCode.CustomerNotFound, "Customer not found.");
        public static readonly ErrorDetails<CustomerErrorCode> DuplicateCustomer = new(ErrorType.Conflict, CustomerErrorCode.DuplicateCustomer, "Customer already exists.");
        public static readonly ErrorDetails<CustomerErrorCode> EmailAlreadyRegistered = new(ErrorType.Conflict, CustomerErrorCode.EmailAlreadyRegistered, "Email is already registered.");
        public static readonly ErrorDetails<CustomerErrorCode> InvalidAddress = new(ErrorType.Validation, CustomerErrorCode.InvalidAddress, "Customer address is invalid.");
        public static readonly ErrorDetails<CustomerErrorCode> InvalidName = new(ErrorType.Validation, CustomerErrorCode.InvalidName, "Customer Name is invalid.");
        public static readonly ErrorDetails<CustomerErrorCode> InvalidEmail = new(ErrorType.Validation, CustomerErrorCode.InvalidEmail, "Customer Email is invalid.");

    }


}
