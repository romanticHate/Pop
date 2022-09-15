using EvilCorp.Pop.Application.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EvilCorp.Pop.Application.Services
{
    public class IdentityService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly byte[] _key;
        public IdentityService(IOptions<JwtSettings> jwtOptions)
        {
            _jwtSettings = jwtOptions.Value;
            _key = Encoding.ASCII.GetBytes(_jwtSettings.SigningKey);
        }
        public JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        public SecurityToken CreateSecuritytoken(ClaimsIdentity identity)
        {
            var tokenDescriptor = GetTokenDescriptor(identity);
            return tokenHandler.CreateToken(tokenDescriptor);           
        }
        public string WriteToken(SecurityToken token)
        {
            return tokenHandler.WriteToken(token);
        }
        private SecurityTokenDescriptor GetTokenDescriptor(ClaimsIdentity identity)
        {
            return new SecurityTokenDescriptor()
            {
                Subject = identity,
                Expires = DateTime.UtcNow.AddHours(2),
                Audience = _jwtSettings.Audiences[0],
                Issuer = _jwtSettings.Issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_key),
                SecurityAlgorithms.HmacSha256Signature)
            };
        }
    }
}
