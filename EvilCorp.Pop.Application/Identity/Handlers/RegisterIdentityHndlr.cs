using EvilCorp.Pop.Application.Enum;
using EvilCorp.Pop.Application.Identity.Commands;
using EvilCorp.Pop.Application.Models;
using EvilCorp.Pop.Application.Options;
using EvilCorp.Pop.Domain.Aggregates.UserProfile;
using EvilCorp.Pop.Domain.Exceptions;
using EvilCorp.Pop.Infrastructure.Sql;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EvilCorp.Pop.Application.Identity.Handlers
{
    public class RegisterIdentityHndlr : IRequestHandler<RegisterIdentity, OperationResult<string>>
    {
        private readonly DataContext _ctx;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        public RegisterIdentityHndlr(IOptions<JwtSettings> jwtSettings,UserManager<IdentityUser> userManager, DataContext ctx)
        {
            _jwtSettings = jwtSettings.Value;
            _userManager = userManager;
            _ctx = ctx;
        }
        public async Task<OperationResult<string>> Handle(RegisterIdentity request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<string>();
            try
            {               
                var existingIdentity = _userManager.FindByNameAsync(request.Username);
                if (existingIdentity.Result != null)
                {
                    result.IsError = true;
                    var error = new Error
                    {
                        Code = ErrorCode.IdentityUserAlreadyExists,
                        Message = $"Provided UserName already exists. Cannot register new User"
                    };
                    result.Errors.Add(error);
                }               

                var identity = new IdentityUser
                {
                    Email=request.EmailAddress,
                    UserName=request.Username
                };
                using var transaction = _ctx.Database.BeginTransaction();
                var createdIdentity = await _userManager.CreateAsync(identity, request.Password);
                if (!createdIdentity.Succeeded)
                {
                    //createdIdentity.Errors
                    result.IsError = true;

                    foreach (var identityError in createdIdentity.Errors)
                    {
                        var error = new Error
                        {
                            Code = ErrorCode.IdentityCreationFailed,
                            Message = identityError.Description
                        };
                        result.Errors.Add(error);
                    }
                    return result;
                }
                var basicInfo = BasicInfo.CreateBasicInfo(request.FirstName, request.LastName, request.EmailAddress,
                       request.Phone, request.DateOfBirth, request.CurrentCity);

                var userProfile = Domain.Aggregates.UserProfile.UserProfile.CreateUserProfile(identity.Id, basicInfo);
                try
                {
                    _ctx.UserProfiles.Add(userProfile);
                    await _ctx.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch(Exception e) 
                {
                    await transaction.RollbackAsync();
                    throw;
                }               

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtSettings.SigningKey);

                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, identity.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, identity.Email),
                        new Claim("IdentityId", identity.Id),
                        new Claim("UserProfileId", userProfile.UserProfileId.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    Audience = _jwtSettings.Audiences[0],
                    Issuer = _jwtSettings.Issuer,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                result.Payload = tokenHandler.WriteToken(token);
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
                        Message = $"{ex.Message}"
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
