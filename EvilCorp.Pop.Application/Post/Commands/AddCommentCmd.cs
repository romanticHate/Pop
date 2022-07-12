using EvilCorp.Pop.Application.Models;
using EvilCorp.Pop.Domain.Aggregates.Post;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilCorp.Pop.Application.Post.Commands
{
    public class AddCommentCmd:IRequest<OperationResult<PostComment>>
    {
        public Guid PostId { get; set; }
        public Guid UserProfileId { get; set; }
        public string Text { get; set; }
    }
}
