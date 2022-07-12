using EvilCorp.Pop.Application.Enum;
using EvilCorp.Pop.Application.Models;
using EvilCorp.Pop.Application.Post.Commands;
using EvilCorp.Pop.Domain.Exceptions;
using EvilCorp.Pop.Infrastructure.Sql;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EvilCorp.Pop.Application.Post.CommandHandlers
{
    public class UpdatePostTextHndlr : IRequestHandler<UpdatePostTextCmd, OperationResult<Domain.Aggregates.Post.Post>>
    {
        private readonly DataContext _ctx;
        public UpdatePostTextHndlr(DataContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<OperationResult<Domain.Aggregates.Post.Post>> Handle(UpdatePostTextCmd request, CancellationToken cancellationToken)
        {
           var result = new OperationResult<Domain.Aggregates.Post.Post>();

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
                }
                post.UpdatePostText(request.Text);
                await _ctx.SaveChangesAsync();
                result.Payload = post;
               
            }
            catch (PostNotValidException ex)
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
