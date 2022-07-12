using EvilCorp.Pop.Application.Models;
using EvilCorp.Pop.Application.UserProfile.Queries;
using EvilCorp.Pop.Infrastructure.Sql;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EvilCorp.Pop.Application.UserProfile.QueryHandlers
{
    internal class GetAllUserProfilesHndlr : IRequestHandler<GetAllUserProfilesQry,
        OperationResult<IEnumerable<Domain.Aggregates.UserProfile.UserProfile>>>
    {
        private readonly DataContext _ctx;
        public GetAllUserProfilesHndlr(DataContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<OperationResult<IEnumerable<Domain.Aggregates.UserProfile.UserProfile>>> Handle(GetAllUserProfilesQry request, 
            CancellationToken cancellationToken)
        {
            var result = new OperationResult<IEnumerable<Domain.Aggregates.UserProfile.UserProfile>>();
            result.Payload = await _ctx.UserProfiles.ToListAsync();
            return result;
        }
    }
}
