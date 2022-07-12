using EvilCorp.Pop.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilCorp.Pop.Application.Post.Querys
{
    public class GetPostCommentsQry:IRequest<OperationResult<List<Domain.Aggregates.Post.PostComment>>>
    {
        public Guid PostId { get; set; }
    }
}
