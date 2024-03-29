﻿using EvilCorp.Pop.Application.Enum;
using EvilCorp.Pop.Application.Identity.Commands;
using EvilCorp.Pop.Application.Models;
using EvilCorp.Pop.Application.Options;
using EvilCorp.Pop.Application.Services;
using EvilCorp.Pop.Infrastructure.Sql;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EvilCorp.Pop.Application.Identity.Handlers
{
    public class LoginHndlr : IRequestHandler<LoginCmd, OperationResult<string>>
    {
        private readonly DataContext _ctx;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly IdentityService _identityService;
       
        public LoginHndlr(IdentityService identityService, IOptions<JwtSettings> options, 
            UserManager<IdentityUser>  userManager, DataContext ctx)
        {
            _ctx = ctx;
            _userManager = userManager;
            _jwtSettings = options.Value;                   
            _identityService = identityService;
        }

        public async Task<OperationResult<string>> Handle(LoginCmd request, 
            CancellationToken cancellationToken)
        {
            var result = new OperationResult<string>();
            try
            {
                var userIdentity = await _userManager.FindByEmailAsync(request.Username);
                    if (userIdentity is null) return result;

                var identity = await ValidateIdentity(result, request, userIdentity);
                    if (!identity) return result; 
                             
                result.Payload = await MakeTokenAsync(userIdentity);
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
                return result;
            }           
        }

        public async Task<bool> ValidateIdentity(OperationResult<string> result,
            LoginCmd request, IdentityUser userIdentity)
        {
            if (userIdentity is null)
            {
                result.IsError = true;
                var error = new Error
                {
                    Code = ErrorCode.IdentityUserDoesNotExist,
                    Message = ErrorMessage.NonExistentIdentityUser
                };

                result.Errors.Add(error);
                return false;
            }

            var validPassword = await _userManager.CheckPasswordAsync(userIdentity,
              request.Password);

            if (!validPassword)
            {
                result.IsError = true;
                var error = new Error
                {
                    Code = ErrorCode.ValidationError,
                    Message = ErrorMessage.IncorrectPassword
                };

                result.Errors.Add(error);
                return false;
            }

            return true;
        }

        public async Task<string> MakeTokenAsync(IdentityUser userIdentity)
        {
            var userProfile = await _ctx.UserProfiles
                     .FirstOrDefaultAsync(up => up.IdentityId == userIdentity.Id);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.SigningKey);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim(JwtRegisteredClaimNames.Sub, userIdentity.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, userIdentity.Email),
                        new Claim("IdentityId", userIdentity.Id),
                        new Claim("UserProfileId", userProfile.UserProfileId.ToString())
                }),

                Expires = DateTime.UtcNow.AddHours(2),
                Audience = _jwtSettings.Audiences[0],
                Issuer = _jwtSettings.Issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }       
    }
}
