using AutoMapper;
using EvilCorp.Pop.Api.Contracts.UserProfile.Requests;
using EvilCorp.Pop.Api.Contracts.UserProfile.Responses;
using EvilCorp.Pop.Api.Filters;
using EvilCorp.Pop.Application.UserProfile.Commands;
using EvilCorp.Pop.Application.UserProfile.Queries;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvilCorp.Pop.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route(ApiRoute.BaseRoute)]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserProfileController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UserProfileController(IMapper mapper,IMediator mediator)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        // GET: api/<UserProfileController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // throw new NotImplementedException("Method not implemented");
            var query = new GetAllUserProfilesQry();
            var response = await _mediator.Send(query);
            var userProfileLst = _mapper.Map<List<UserProfileRspn>>(response.Payload);
              if (response.IsError) { return HandleErrorResponse(response.Errors); }
            return Ok(userProfileLst);
        }

        // GET api/<UserProfileController>/5
        [Route(ApiRoute.UserProfile.RouteId)]
        [HttpGet]
        [ValidateGuid("id")]
        public async Task<IActionResult> GetById(string id)
        {
            var query = new GetByIdUserProfileQry { UserProfileId = Guid.Parse(id) };
            var response = await _mediator.Send(query);

            if (response.IsError) { return HandleErrorResponse(response.Errors); }

            var userProfile = _mapper.Map<UserProfileRspn>(response.Payload);
            return Ok(userProfile);
        }

        // POST api/<UserProfileController>
        [HttpPost]      
        public async Task<IActionResult> Create([FromBody] UserProfileRqst userProfile)
        {
            var command = _mapper.Map<CreateUserProfileCmd>(userProfile);
            var response = await _mediator.Send(command);
            //if(response.IsError) { return HandleErrorResponse(response.Errors); }

            var userProfileObj = _mapper.Map<UserProfileRspn>(response.Payload);
            //return CreatedAtAction(nameof(GetById), new { id = userProfileObj.UserProfileId }, userProfileObj);
            return response.IsError ? HandleErrorResponse(response.Errors) : CreatedAtAction(nameof(GetById),
                   new { id = userProfileObj.UserProfileId }, userProfileObj);
        }     

        // PUT api/<UserProfileController>/5
        [Route(ApiRoute.UserProfile.RouteId)]
        [HttpPut]
        [ValidateModel]
        [ValidateGuid("id")]
        public async Task<IActionResult> Put(string id, [FromBody] UserProfileRqst userProfile)
        {
            var command = _mapper.Map<UpdateBasicInfoCmd>(userProfile);
            command.UserProfileId = Guid.Parse(id);
            var response = await _mediator.Send(command);
            //if (response.IsError) return HandleErrorResponse(response.Errors);
            var userProfileObj = _mapper.Map<UserProfileRspn>(response.Payload);           
            return response.IsError ? HandleErrorResponse(response.Errors) : CreatedAtAction(nameof(GetById),
                   new { id = userProfileObj.UserProfileId }, userProfileObj);
        }

        // DELETE api/<UserProfileController>/5
        [Route(ApiRoute.UserProfile.RouteId)]
        [HttpDelete]
        [ValidateGuid("id")]
        public async Task<IActionResult> Delete(string id)
        {
            var command = new DeleteUserProfileCmd() { UserProfileId = Guid.Parse(id) };
            var response = await _mediator.Send(command);
            return response.IsError ? HandleErrorResponse(response.Errors) : NoContent();
        }       
    }
}
