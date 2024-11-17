
namespace Infrastructure.PostgressExceptions.Exceptions
{
   public class MaxLengthExceededException : PostgressBaseException
    {
        internal MaxLengthExceededException(string tablename, string errorcode, string severity, string message, Exception exception) : base(tablename, errorcode, severity, message, exception)
        {
        }
    }
}
