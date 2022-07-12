using EvilCorp.Pop.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilCorp.Pop.Application.UserProfile.Queries
{
    public class GetByIdUserProfileQry:IRequest<OperationResult<Domain.Aggregates.UserProfile.UserProfile>>
    {
        public Guid UserProfileId { get; set; }
    }
}
