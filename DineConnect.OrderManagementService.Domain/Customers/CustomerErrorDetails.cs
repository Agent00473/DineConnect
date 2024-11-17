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
        MissingData = 8
    }

    ///<summary>
    ///Custom Error instance to be returned with MediatR Command / Query Validator errors.
    ///Custom state that returnes when validation fails for a particular rule. 
    ///</summary>
    public sealed record CustomerErrorDetails
    {
        public static readonly ErrorDetails<CustomerErrorCode> None = new(CustomerErrorCode.None, string.Empty);
        public static readonly ErrorDetails<CustomerErrorCode> CustomerNotFound = new(CustomerErrorCode.CustomerNotFound, "Customer not found.");
        public static readonly ErrorDetails<CustomerErrorCode> DuplicateCustomer = new(CustomerErrorCode.DuplicateCustomer, "Customer already exists.");
        public static readonly ErrorDetails<CustomerErrorCode> EmailAlreadyRegistered = new(CustomerErrorCode.EmailAlreadyRegistered, "Email is already registered.");
        public static readonly ErrorDetails<CustomerErrorCode> InvalidCustomerId = new(CustomerErrorCode.InvalidCustomerId, "Customer ID is not valid.");
        public static readonly ErrorDetails<CustomerErrorCode> InvalidAddress = new(CustomerErrorCode.InvalidAddress, "Customer address is invalid.");
        public static readonly ErrorDetails<CustomerErrorCode> InvalidName = new(CustomerErrorCode.InvalidName, "Customer Name is invalid.");
        public static readonly ErrorDetails<CustomerErrorCode> InvalidEmail = new(CustomerErrorCode.InvalidEmail, "Customer Email is invalid.");
        public static readonly ErrorDetails<CustomerErrorCode> NullData = new(CustomerErrorCode.MissingData, "Data cannot be Empty.");

    }


}
