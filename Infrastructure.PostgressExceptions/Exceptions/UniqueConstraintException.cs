
namespace Infrastructure.PostgressExceptions.Exceptions
{
    public class UniqueConstraintException: PostgressBaseException
    {
        public string ConstraintName { get; internal set; }

        internal  UniqueConstraintException(string tablename, string errorcode, string severity, string message, Exception exception) 
                                                    : base(tablename, errorcode, severity, message, exception)
        {
        }

    }
}
