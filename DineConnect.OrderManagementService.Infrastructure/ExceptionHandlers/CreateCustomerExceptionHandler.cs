using Infrastructure.PostgressExceptions;
using Infrastructure.PostgressExceptions.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DineConnect.OrderManagementService.Infrastructure.ExceptionHandlers
{
    public class CreateCustomerExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
            CancellationToken cancellationToken)
        {
            if (exception is PostgressBaseException pex)
            {
                var problemDetails = new ProblemDetails
                {
                    Title = pex.Message,
                    Status = (int)HttpStatusCode.InternalServerError,
                    Detail = $"Error in {pex.TableName} Severity {pex.Severity}",
                    Instance = httpContext.Request.Path
                };
                if (exception is UniqueConstraintException uex)
                {
                    problemDetails.Detail = $"Severity: {uex.Severity}: Unique Constraint violation for  Constraint: {uex.ConstraintName}. Table: {uex.TableName}";
                }

                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);
                return true;
            }
            return false;
        }
    }
}
