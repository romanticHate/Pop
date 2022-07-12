using EvilCorp.Pop.Application.Enum;
using EvilCorp.Pop.Application.Models;
using EvilCorp.Pop.Application.Post.Querys;
using EvilCorp.Pop.Domain.Aggregates.Post;
using EvilCorp.Pop.Infrastructure.Sql;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EvilCorp.Pop.Application.Post.QueryHandlers
{
    public class GetPostCommentsHndlr : IRequestHandler<GetPostCommentsQry, OperationResult<List<PostComment>>>
    {
        private readonly DataContext _ctx;
        public GetPostCommentsHndlr(DataContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<OperationResult<List<PostComment>>> Handle(GetPostCommentsQry request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<List<PostComment>>();
            try
            {
                var posts = await _ctx.Posts
                    .Include(p => p.Comments)
                    .FirstOrDefaultAsync(p => p.PostId == request.PostId);

                result.Payload = posts.Comments.ToList();
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
