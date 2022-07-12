using EvilCorp.Pop.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilCorp.Pop.Application.Post.Querys
{
    public class GetAllPostQry:IRequest<OperationResult<List<Domain.Aggregates.Post.Post>>>
    {

    }
}
