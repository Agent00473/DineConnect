
namespace Infrastructure.PostgressExceptions.Exceptions
{
    public class NumericOverflowException : PostgressBaseException
    {
        internal NumericOverflowException(string tablename, string errorcode, string severity, string message, Exception exception) : base(tablename, errorcode, severity, message, exception)
        {
        }
    }
}

