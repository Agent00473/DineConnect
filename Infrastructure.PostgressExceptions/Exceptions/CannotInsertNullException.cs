

namespace Infrastructure.PostgressExceptions.Exceptions
{
    public class CannotInsertNullException : PostgressBaseException
    {
        internal CannotInsertNullException(string tablename, string errorcode, string severity, string message, Exception exception) : base(tablename, errorcode, severity, message, exception)
        {
        }
    }
}
