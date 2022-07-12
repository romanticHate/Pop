using AutoMapper;
using EvilCorp.Pop.Api.Contracts.Identity;
using EvilCorp.Pop.Api.Filters;
using EvilCorp.Pop.Application.Identity.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EvilCorp.Pop.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route(ApiRoute.BaseRoute)]
    [ApiController]
    public class IdentityController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public IdentityController(IMapper mapper, IMediator mediator)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        
        [HttpPost]
        [Route(ApiRoute.Identity.Registration)]
        [ValidateModel]
        public async Task<IActionResult> Register([FromBody] UserRegistration userRegistration)
        {
            var command = _mapper.Map<RegisterIdentity>(userRegistration);

            var response = await _mediator.Send(command);
            if (response.IsError) return HandleErrorResponse(response.Errors);

            var authRslt = new AuthenticationResult() { Token = response.Payload };
            return Ok(authRslt);
        }        
    }
}
