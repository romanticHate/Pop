using EvilCorp.Pop.Application.Enum;
using EvilCorp.Pop.Application.Models;
using EvilCorp.Pop.Application.UserProfile.Commands;
using EvilCorp.Pop.Domain.Exceptions;
using EvilCorp.Pop.Infrastructure.Sql;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EvilCorp.Pop.Application.UserProfile.CommandHandlers
{
    internal class DeleteUserProfileCmdHndlr : IRequestHandler<DeleteUserProfileCmd, 
        OperationResult<Domain.Aggregates.UserProfile.UserProfile>>
    {
        private readonly DataContext _ctx;
        public DeleteUserProfileCmdHndlr(DataContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<OperationResult<Domain.Aggregates.UserProfile.UserProfile>> Handle(DeleteUserProfileCmd request,
            CancellationToken cancellationToken)
        {
            var result = new OperationResult<Domain.Aggregates.UserProfile.UserProfile>();
            try
            {
                var userProfile = await _ctx.UserProfiles
                .FirstOrDefaultAsync(up => up.UserProfileId == request.UserProfileId);

                if (userProfile is null)
                {
                    result.IsError = true;
                    var error = new Error
                    {
                        Code = ErrorCode.NotFound,
                        Message = $"No UserProfile found whit ID: {request.UserProfileId}"
                    };
                    result.Errors.Add(error);
                    return result;
                }

                _ctx.UserProfiles.Remove(userProfile);
                await _ctx.SaveChangesAsync();

                result.Payload = userProfile;
            }
            catch (UserProfileNotValidException ex)
            {
                result.IsError = true;
                ex.Errors.ForEach(e =>
                {
                    var error = new Error
                    {
                        Code = ErrorCode.ValidationError,
                        Message = $"{ex.Message}"
                    };

                    result.Errors.Add(error);
                });
                return result;
            }
            catch (Exception e)
            {
                var error = new Error
                {
                    Message = e.Message,
                    Code = ErrorCode.ServerError
                };
                result.IsError = true;
                result.Errors.Add(error);
            }

            return result;
        }
    }
}
