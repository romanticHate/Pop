using EvilCorp.Pop.Application.Enum;
using EvilCorp.Pop.Application.Models;
using EvilCorp.Pop.Application.UserProfile.Commands;
using EvilCorp.Pop.Domain.Aggregates.UserProfile;
using EvilCorp.Pop.Domain.Exceptions;
using EvilCorp.Pop.Infrastructure.Sql;
using MediatR;

namespace EvilCorp.Pop.Application.UserProfile.CommandHandlers
{
    public class CreateUserProfileCmdHndlr : IRequestHandler<CreateUserProfileCmd,
         OperationResult<Domain.Aggregates.UserProfile.UserProfile>>
    {        
        private readonly DataContext _ctx;
        public CreateUserProfileCmdHndlr(DataContext ctx)
        {          
           _ctx = ctx;
        }
        public async Task<OperationResult<Domain.Aggregates.UserProfile.UserProfile>> Handle(CreateUserProfileCmd request,
            CancellationToken cancellationToken)
        {
            var result = new OperationResult<Domain.Aggregates.UserProfile.UserProfile>();
            try
            {
                var basicInfo = BasicInfo.CreateBasicInfo(request.FirstName, request.LastName, request.EmailAddress,
                    request.Phone, request.DateOfBirth, request.CurrentCity);

                var userProfile = Domain.Aggregates.UserProfile.UserProfile.CreateUserProfile(Guid.NewGuid().ToString(), basicInfo);

                _ctx.UserProfiles.Add(userProfile);
                await _ctx.SaveChangesAsync();

                result.Payload = userProfile;
                return result;
            }
            catch (UserProfileNotValidException ex)
            {
                result.IsError = true;
                ex.Errors.ForEach(e =>
                {
                    var error = new Error 
                    {  
                        Code = ErrorCode.ValidationError,
                         Message=$"{ex.Message}"
                    };

                    result.Errors.Add(error);
                });              
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
