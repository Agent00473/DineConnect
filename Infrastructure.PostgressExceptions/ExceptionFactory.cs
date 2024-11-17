using Infrastructure.PostgressExceptions.Exceptions;
using Npgsql;
using System;

namespace Infrastructure.PostgressExceptions
{
    public static class ExceptionFactory
    {
        private static string GetValueOrDefault(string? data, string alternate)
        {
            return string.IsNullOrEmpty(data) ? alternate : data; 
        }

        private static UniqueConstraintException CreateUniqueConstraintException(string errorCode, string entityName, string message, PostgresException exception)
        {
            UniqueConstraintException result = new UniqueConstraintException(GetValueOrDefault(exception.TableName, entityName), 
                                                                    errorCode, exception.Severity, message, exception)
            {
                ConstraintName = GetValueOrDefault(exception.ConstraintName, "Unique Constraint Error"),
            };
            return result;
        }

        private static CannotInsertNullException CreateCannotInsertNullException(string errorCode, string entityName, string message, PostgresException exception)
        {
            var result = new CannotInsertNullException(GetValueOrDefault(exception.TableName, entityName),
                                                                   errorCode, exception.Severity, message, exception);
            return result;
        }

        private static ReferenceConstraintException CreateReferenceConstraintException(string errorCode, string entityName, string message, PostgresException exception)
        {
            var result = new ReferenceConstraintException(GetValueOrDefault(exception.TableName, entityName),
                                                                   errorCode, exception.Severity, message, exception);
            return result;
        }

        private static MaxLengthExceededException CreateMaxLengthExceededException(string errorCode, string entityName, string message, PostgresException exception)
        {
            var result = new MaxLengthExceededException(GetValueOrDefault(exception.TableName, entityName),
                                                                   errorCode, exception.Severity, message, exception);
            return result;
        }

        private static NumericOverflowException CreateNumericOverflowException(string errorCode, string entityName, string message, PostgresException exception)
        {
            var result = new NumericOverflowException(GetValueOrDefault(exception.TableName, entityName),
                                                                   errorCode, exception.Severity, message, exception);
            return result;
        }

        private static GeneralException CreateGeneralException(string errorCode, string entityName, string message, Exception exception)
        {
            var result = new GeneralException(entityName,errorCode, "Error", message, exception);
            return result;
        }
        private static GeneralException CreateGeneralException(string errorCode, string entityName, string message, PostgresException exception)
        {
            var result = new GeneralException(GetValueOrDefault(exception.TableName, entityName),
                                                                   errorCode, exception.Severity, message, exception);
            return result;
        }

        public static PostgressBaseException GetDatabaseError(Exception exception, string entityName, string message)
        {
            if (exception.InnerException is PostgresException dbException)
            {

                return dbException.SqlState switch
                {
                    PostgresErrorCodes.StringDataRightTruncation => CreateMaxLengthExceededException(PostgresErrorCodes.StringDataRightTruncation, entityName, message, dbException),
                    PostgresErrorCodes.NumericValueOutOfRange => CreateNumericOverflowException(PostgresErrorCodes.NumericValueOutOfRange, entityName, message, dbException),
                    PostgresErrorCodes.NotNullViolation => CreateCannotInsertNullException(PostgresErrorCodes.NotNullViolation, entityName, message, dbException),
                    PostgresErrorCodes.UniqueViolation => CreateUniqueConstraintException(PostgresErrorCodes.UniqueViolation, entityName, message, dbException),
                    PostgresErrorCodes.ForeignKeyViolation => CreateReferenceConstraintException(PostgresErrorCodes.ForeignKeyViolation, entityName, message, dbException),
                    _ => CreateGeneralException("UNKNOWN", entityName, message, dbException)
                };
            }
            return new GeneralException(entityName, "Unknown", "Error", message, exception);

        }

    }
}
