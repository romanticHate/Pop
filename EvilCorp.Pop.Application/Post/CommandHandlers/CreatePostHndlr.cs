using EvilCorp.Pop.Application.Enum;
using EvilCorp.Pop.Application.Models;
using EvilCorp.Pop.Application.Post.Commands;
using EvilCorp.Pop.Domain.Exceptions;
using EvilCorp.Pop.Infrastructure.Sql;
using MediatR;

namespace EvilCorp.Pop.Application.Post.CommandHandlers
{
    public class CreatePostHndlr : IRequestHandler<CreatePostCmd, OperationResult<Domain.Aggregates.Post.Post>>
    {
        private readonly DataContext _ctx;
        public CreatePostHndlr(DataContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<OperationResult<Domain.Aggregates.Post.Post>> Handle(CreatePostCmd request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Domain.Aggregates.Post.Post>();
            try
            {
                var post = Domain.Aggregates.Post.Post.CreatePost(request.UserProfileId, request.TextContent);
                _ctx.Posts.Add(post);
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
