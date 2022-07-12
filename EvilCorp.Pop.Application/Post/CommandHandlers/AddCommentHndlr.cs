using EvilCorp.Pop.Application.Enum;
using EvilCorp.Pop.Application.Models;
using EvilCorp.Pop.Application.Post.Commands;
using EvilCorp.Pop.Domain.Aggregates.Post;
using EvilCorp.Pop.Domain.Exceptions;
using EvilCorp.Pop.Infrastructure.Sql;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EvilCorp.Pop.Application.Post.CommandHandlers
{
    public class AddCommentHndlr : IRequestHandler<AddCommentCmd, OperationResult<PostComment>>
    {
        private readonly DataContext _ctx;
        public AddCommentHndlr(DataContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<OperationResult<PostComment>> Handle(AddCommentCmd request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PostComment>();
            try
            {
                var post = await _ctx.Posts.FirstOrDefaultAsync(p => p.PostId == request.PostId);
                if (post is null)
                {
                    result.IsError = true;
                    var error = new Error
                    {
                        Code = ErrorCode.NotFound,
                        Message = $"No post found whit ID {request.PostId}"
                    };
                    result.Errors.Add(error);
                    return result;
                }
                var comment = PostComment.CreatePostComment(request.PostId, request.Text, request.UserProfileId);
                post.AddPostComment(comment);
                _ctx.Posts.Update(post);
                await _ctx.SaveChangesAsync();
                result.Payload = comment;

            }           
            catch (PostCommentNotValidException ex)
            {
                result.IsError = true;
                ex.Errors.ForEach(e =>
                {
                    var error = new Error
                    {
                        Code = ErrorCode.ValidationError,
                        Message = $"{ex.Message}"
                    };

                    result.Errors.Add(error);
                });
            }
            catch (Exception e)
            {
                var error = new Error
                {
                    Message = e.Message,
                    Code = ErrorCode.ServerError
                };
                result.IsError = true;
                result.Errors.Add(error);
            }
            return result;
        }
    }
}
