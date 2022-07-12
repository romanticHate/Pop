using EvilCorp.Pop.Application.Models;
using MediatR;

namespace EvilCorp.Pop.Application.UserProfile.Queries
{
    public class GetAllUserProfilesQry:IRequest<OperationResult<IEnumerable<Domain.Aggregates.UserProfile.UserProfile>>>
    {

    }
}
