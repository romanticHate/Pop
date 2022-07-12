using EvilCorp.Pop.Application.Models;
using MediatR;

namespace EvilCorp.Pop.Application.Post.Commands
{
    public class UpdatePostTextCmd:IRequest<OperationResult<Domain.Aggregates.Post.Post>>
    {
        public Guid PostId { get; set; }
     
        public string Text { get; set; }
      
    }
}
