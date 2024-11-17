using Npgsql;

namespace Infrastructure.PostgressExceptions
{
    public class PostgressBaseException: Exception
    {
        public Exception Error { get; private set; }
        public string TableName { get; private set; }
        public string ErrorCode { get; private set; }
        public string Severity { get; private set; }
  
        protected PostgressBaseException(string tablename, string errorcode, string severity, string message, Exception exception):base(message) 
        {
            Error = exception;
            TableName = tablename;
            ErrorCode = errorcode;
            Severity = severity;
        }
       

    }
}
