﻿using AutoMapper;
using EvilCorp.Pop.Api.Contracts.Common;
using EvilCorp.Pop.Api.Contracts.Post.Requests;
using EvilCorp.Pop.Api.Contracts.Post.Responses;
using EvilCorp.Pop.Api.Filters;
using EvilCorp.Pop.Application.Post.Commands;
using EvilCorp.Pop.Application.Post.Querys;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;
using EvilCorp.Pop.Api.Extensions;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EvilCorp.Pop.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route(ApiRoute.BaseRoute)]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PostController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public PostController(IMapper mapper, IMediator mediator)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        // GET: api/<PostController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {           
           var response = await _mediator.Send(new GetAllPostQry());
           var postLst = _mapper.Map<List<PostRspn>>(response.Payload);
           return response.IsError ? HandleErrorResponse(response.Errors) : Ok(postLst);
        }

        // GET api/<PostController>/5
        [HttpGet(ApiRoute.Post.RouteId)]
        [ValidateGuid("id")]// Action filter
        public async Task<IActionResult> GetById(string id)
        {
            var query = new GetByIdPostQry { PostId = Guid.Parse(id) };
            var response = await _mediator.Send(query);
            var postObj = _mapper.Map<PostRspn>(response.Payload);
            return response.IsError ? HandleErrorResponse(response.Errors) : Ok(postObj);
        }

        // POST api/<UserProfileController>
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] PostRqst postRqst)
        {
            // var identity = HttpContext.User.Identity as ClaimsIdentity;
            // var userProfileId = identity?.FindFirst("UserProfileId")?.Value;
            var command = new CreatePostCmd
            {
                UserProfileId = HttpContext.GetUserProfileIdClaim(),
                TextContent = postRqst.TextContent
            };
            //var command = _mapper.Map<CreatePostCmd>(postRqst);
            var response = await _mediator.Send(command);
            if (response.IsError) { return HandleErrorResponse(response.Errors); }
            var postRspn = _mapper.Map<PostRspn>(response.Payload);
            return CreatedAtAction(nameof(GetById), new { id = postRspn.PostId }, postRspn);            
        }

        // PUT api/<UserProfileController>/5
        [Route(ApiRoute.Post.RouteId)]
        [HttpPut]
        [ValidateModel]// Action filter
        [ValidateGuid("id")]
        public async Task<IActionResult> Put(string id, [FromBody] PostRqst postRqst)
        {
            var command = new UpdatePostTextCmd
            {
                PostId = Guid.Parse(id),
                Text = postRqst.TextContent,
                UserProfileId = HttpContext.GetUserProfileIdClaim()
            };

            var response = await _mediator.Send(command);            
            var postRspn = _mapper.Map<PostRspn>(response.Payload);            
            return response.IsError ? HandleErrorResponse(response.Errors) : CreatedAtAction(nameof(GetById),
                  new { id = postRspn.PostId }, postRspn);
        }

        // DELETE api/<UserProfileController>/5
        [Route(ApiRoute.UserProfile.RouteId)]
        [HttpDelete]
        [ValidateGuid("id")]// Action filter
        public async Task<IActionResult> Delete(string id)
        {
            var command = new DeletePostCmd() 
            { 
                PostId = Guid.Parse(id), 
                UserProfileId = HttpContext.GetUserProfileIdClaim() 
            };

            var response = await _mediator.Send(command);
            var postObj = _mapper.Map<PostRspn>(response.Payload);
            return response.IsError ? HandleErrorResponse(response.Errors) : NoContent();
        }

        [Route(ApiRoute.Post.Comments)]
        [HttpPost]
        [ValidateGuid("postId")]// Action filter
        public async Task<IActionResult> AddCommentToPost(string postId, [FromBody] CommentRqst commentRqst)
        {
            var isValidGuid = Guid.TryParse(commentRqst.UserProfileId, out var userProfileId);
            if (!isValidGuid)
            {
                var apiError = new ErrorResponse();
                apiError.StatusCode = 400;
                apiError.StatusPhrase = "Bad Request";
                apiError.TimeStamp = DateTime.UtcNow;
                apiError.Errors.Add("Provited User profile ID is not a valid GUID format !");

                return BadRequest(apiError);
            }
            var command = new AddCommentCmd()
            {
                PostId = Guid.Parse(postId),
                Text = commentRqst.Text,
                UserProfileId = userProfileId
            };
            var response = await _mediator.Send(command);
            var commentRspn = _mapper.Map<CommentRspn>(response.Payload);
            return response.IsError ? HandleErrorResponse(response.Errors) : Ok(commentRspn);
        }

        [Route(ApiRoute.Post.Comments)]
        [HttpGet]
        [ValidateModel]// Action filter
        [ValidateGuid("postId")]// Action filter
        public async Task<IActionResult> CommentsByPostId(string postId)
        {
            var query = new GetPostCommentsQry() { PostId = Guid.Parse(postId) };
            var response = await _mediator.Send(query);
            var comments = _mapper.Map<List<CommentRspn>>(response.Payload);
            return response.IsError ? HandleErrorResponse(response.Errors) : Ok(comments);
        }
    }
}
