
namespace Infrastructure.PostgressExceptions.Exceptions
{
    public class ReferenceConstraintException : PostgressBaseException
    {
        internal ReferenceConstraintException(string tablename, string errorcode, string severity, string message, Exception exception) : base(tablename, errorcode, severity, message, exception)
        {
        }
    }
}
