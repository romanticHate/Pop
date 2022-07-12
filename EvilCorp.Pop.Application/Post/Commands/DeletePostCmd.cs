using EvilCorp.Pop.Application.Models;
using MediatR;

namespace EvilCorp.Pop.Application.Post.Commands
{
    public class DeletePostCmd : IRequest<OperationResult<Domain.Aggregates.Post.Post>>
    {
        public Guid PostId { get; set; }
    }
}
