namespace DineConnect.PaymentManagementService.Domain.Common
{
    public enum Transaction_Status
    {
        Unknown = 0,
        Pending,
        Success,
        Failed
    }

    public enum Payment_Status
    {
        Unknown = 0,
        Pending,
        Completed,
        Failed,
    }

    public enum Invoice_Status
    {
        Unknown = 0,
        Pending,
        Paid,
        Overdue,
        Refunded,
        Closed
    }

    public enum Refund_Status
    {
        Unknown = 0,
        Pending,
        Approved,
        Denied
    }

    public enum Payment_Gateway
    {
        CreditCard,
        PayPal,
        BankTransfer
    }

    public enum Refund_Reason
    {
        CustomerRequest,
        PaymentError,
        ProductReturn
    }

    public enum Transaction_Category
    {
        Unknown = 0,
        Credit,
        Refund
        
    }

}
