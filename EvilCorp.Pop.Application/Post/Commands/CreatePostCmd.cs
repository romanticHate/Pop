using EvilCorp.Pop.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilCorp.Pop.Application.Post.Commands
{
    public class CreatePostCmd:IRequest<OperationResult<Domain.Aggregates.Post.Post>>
    {
        public Guid PostId { get; set; }
        public Guid UserProfileId { get; set; }
        public string TextContent { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModified { get; set; }
    }
}
