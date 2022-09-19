using EvilCorp.Pop.Api.Contracts.Common;
using EvilCorp.Pop.Application.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EvilCorp.Pop.Api.Controllers.V1
{
    [Route(ApiRoute.BaseRoute)]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IActionResult HandleErrorResponse(List<Error> errors)
        {
            // filters
            var apiError = new ErrorResponse();
            if (errors.Any(e => e.Code == Application.Enum.ErrorCode.NotFound))
            {
                var error = errors.First(e => e.Code == Application.Enum.ErrorCode.NotFound);
             
                apiError.StatusCode = 404;
                apiError.StatusPhrase = "Not Found";
                apiError.TimeStamp = DateTime.UtcNow;
                apiError.Errors.Add(error.Message);

                return NotFound(apiError);
            }
            apiError.StatusCode = 400;
            apiError.StatusPhrase = "Bad request";
            apiError.TimeStamp = DateTime.UtcNow;
            errors.ForEach(e => apiError.Errors.Add(e.Message));

            return StatusCode(400,apiError);

            //if (errors.Any(e => e.Code == Application.Enum.ErrorCode.ServerError))
            //{
            //    var error = errors.FirstOrDefault(e => e.Code == Application.Enum.ErrorCode.NotFound);
            //    return StatusCode(500, error.Message);
            //}
            //return BadRequest(errors);
        }        
    }
}
