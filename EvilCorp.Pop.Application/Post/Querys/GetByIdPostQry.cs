using EvilCorp.Pop.Application.Models;
using MediatR;

namespace EvilCorp.Pop.Application.Post.Querys
{
    public class GetByIdPostQry:IRequest<OperationResult<Domain.Aggregates.Post.Post>>
    {
        public Guid PostId { get; set; }
    }
}
