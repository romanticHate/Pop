using EvilCorp.Pop.Application.Enum;
using EvilCorp.Pop.Application.Models;
using EvilCorp.Pop.Application.UserProfile.Commands;
using EvilCorp.Pop.Domain.Aggregates.UserProfile;
using EvilCorp.Pop.Domain.Exceptions;
using EvilCorp.Pop.Infrastructure.Sql;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EvilCorp.Pop.Application.UserProfile.CommandHandlers
{
    internal class UpdateBasicInfoCmdHndlr : IRequestHandler<UpdateBasicInfoCmd, OperationResult<Domain.Aggregates.UserProfile.UserProfile>>
    {
        private readonly DataContext _ctx;
        public UpdateBasicInfoCmdHndlr(DataContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<OperationResult<Domain.Aggregates.UserProfile.UserProfile>> Handle(UpdateBasicInfoCmd request, 
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
                        Message = ErrorMessage.ProfileNotFound // $"No UserProfile found whit ID: {request.UserProfileId}"
                    };
                    result.Errors.Add(error);
                    return result;
                }

                var basicInfo = BasicInfo.CreateBasicInfo(request.FirstName, request.LastName,
                    request.EmailAddress, request.Phone, request.DateOfBirth, request.CurrentCity);

                userProfile.UpdateBasicInfo(basicInfo);

                _ctx.UserProfiles.Update(userProfile);
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
