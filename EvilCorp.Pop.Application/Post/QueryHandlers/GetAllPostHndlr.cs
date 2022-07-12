using EvilCorp.Pop.Application.Enum;
using EvilCorp.Pop.Application.Models;
using EvilCorp.Pop.Application.Post.Querys;
using EvilCorp.Pop.Infrastructure.Sql;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EvilCorp.Pop.Application.Post.QueryHandlers
{
    public class GetAllPostHndlr : IRequestHandler<GetAllPostQry, OperationResult<List<Domain.Aggregates.Post.Post>>>
    {
        private readonly DataContext _ctx;
        public GetAllPostHndlr(DataContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<OperationResult<List<Domain.Aggregates.Post.Post>>> Handle(GetAllPostQry request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<List<Domain.Aggregates.Post.Post>>();
            try
            {                
                var post = await _ctx.Posts.ToListAsync();
                result.Payload = post;

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
