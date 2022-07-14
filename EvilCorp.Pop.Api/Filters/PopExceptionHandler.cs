using EvilCorp.Pop.Api.Contracts.Common;
using EvilCorp.Pop.Application.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EvilCorp.Pop.Api.Filters
{
    public class PopExceptionHandler: ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var apiError = new ErrorResponse
            {
                StatusCode = 500,
                StatusPhrase = "Internal Server Error",
                TimeStamp = DateTime.UtcNow
            };           

            apiError.Errors.Add(context.Exception.Message);

            context.Result = new JsonResult(apiError) { StatusCode = 500 };
        }
    }
}
