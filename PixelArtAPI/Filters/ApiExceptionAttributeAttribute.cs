using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PixelArt.Models.Exceptions;

namespace PixelArtAPI.Filters
{
    public class ApiExceptionAttributeAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var baseException = context.Exception.GetBaseException();

            int statusCode = StatusCodes.Status500InternalServerError;
            string message = "Internal server error occurred";

            if (baseException is BaseApiException baseApiException)
            {
                statusCode = baseApiException.ResponseHttpStatusCode;
                message = baseApiException.BackEndMessage;
            }

            context.HttpContext.Response.StatusCode = statusCode;

            context.Result = new JsonResult(new
            {
                statusCode,
                message
            });

            base.OnException(context);
        }
    }
}
