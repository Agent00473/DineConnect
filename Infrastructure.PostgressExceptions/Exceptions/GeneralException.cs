
namespace Infrastructure.PostgressExceptions.Exceptions
{
    internal class GeneralException : PostgressBaseException
    {
        internal GeneralException(string tablename, string errorcode, string severity, string message, Exception exception) : base(tablename, errorcode, severity, message, exception)
        {
        }
    }
}
