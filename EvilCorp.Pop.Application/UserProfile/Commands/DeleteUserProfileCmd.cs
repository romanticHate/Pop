using EvilCorp.Pop.Application.Models;
using MediatR;

namespace EvilCorp.Pop.Application.UserProfile.Commands
{
    public class DeleteUserProfileCmd : IRequest<OperationResult<Domain.Aggregates.UserProfile.UserProfile>>
    {
        public Guid UserProfileId { get; set; }
    }
}
