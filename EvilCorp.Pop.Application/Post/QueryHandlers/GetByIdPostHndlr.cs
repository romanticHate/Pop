using EvilCorp.Pop.Application.Enum;
using EvilCorp.Pop.Application.Models;
using EvilCorp.Pop.Application.Post.Querys;
using EvilCorp.Pop.Infrastructure.Sql;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EvilCorp.Pop.Application.Post.QueryHandlers
{
    public class GetByIdPostHndlr : IRequestHandler<GetByIdPostQry, OperationResult<Domain.Aggregates.Post.Post>>
    {
        private readonly DataContext _ctx;
        public GetByIdPostHndlr(DataContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<OperationResult<Domain.Aggregates.Post.Post>> Handle(GetByIdPostQry request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Domain.Aggregates.Post.Post>();
            var post = await _ctx.Posts
                .FirstOrDefaultAsync(p => p.PostId == request.PostId);

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
            result.Payload = post;
            return result;
        }
    }
}
