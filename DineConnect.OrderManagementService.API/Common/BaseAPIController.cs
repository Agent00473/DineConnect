using DineConnect.OrderManagementService.Application.Common;
using Microsoft.AspNetCore.Mvc;

namespace DineConnect.OrderManagementService.API.Common
{
    public class BaseAPIController : ControllerBase
    {
        private ObjectResult CreateResult<TValue, TErrorCode>(ResponseWrapper<TValue, TErrorCode> result) where TErrorCode : Enum
        {
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            var problemDetails = new ProblemDetails
            {
                Title = "An error occurred",
                Status = result.Error != null ? Convert.ToInt32(result.Error.Code) : null,
                Detail = result.Error?.Message,
                Instance = HttpContext.Request.Path
            };

            return StatusCode(result.Error != null ? Convert.ToInt32(result.Error.Code) : 404, problemDetails);
        }
        protected ActionResult HandleResult<TValue, TErrorCode>(ResponseWrapper<TValue, TErrorCode> result) where TErrorCode : Enum
        {
            return CreateResult(result);
        }


        protected IActionResult Handle<TValue, TErrorCode>(ResponseWrapper<TValue, TErrorCode> result) where TErrorCode : Enum
        {
            return CreateResult(result);
        }

    }
}

