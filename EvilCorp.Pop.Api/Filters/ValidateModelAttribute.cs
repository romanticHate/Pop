using EvilCorp.Pop.Api.Contracts.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EvilCorp.Pop.Api.Filters
{
    public class ValidateModelAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var apiError = new ErrorResponse 
                {
                    StatusCode = 400,
                    StatusPhrase = "Bad request",
                    TimeStamp = DateTime.UtcNow
                };              

                var errors = context.ModelState.AsEnumerable();

                foreach (var error in errors)
                {
                    foreach (var inner in error.Value.Errors)
                    {
                        apiError.Errors.Add(inner.ErrorMessage);
                    }
                }

                context.Result = new BadRequestObjectResult(apiError);
                //context.Result = new NotFoundObjectResult(apiError);
                //context.Result = new JsonResult(apiError) { StatusCode = 400 };

                // TO DO: Make sure Asp.Net Core doesnt ovverride mour action result body
            }
        }
    }
}
