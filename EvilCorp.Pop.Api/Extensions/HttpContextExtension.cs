using System.Security.Claims;

namespace EvilCorp.Pop.Api.Extensions
{
    public static class HttpContextExtension
    {
        public static Guid GetUserProfileIdClaim(this HttpContext context)
        {
            return GetGuidClaim("UserProfileId", context);
            //var identity = context.User.Identity as ClaimsIdentity;           
            //return Guid.Parse(identity?.FindFirst("UserProfileId")?.Value);
        }
        public static Guid GetIdentityIdClaim(this HttpContext context)
        {
            return GetGuidClaim("IdentityId", context);
            //var identity = context.User.Identity as ClaimsIdentity;
            //return Guid.Parse(identity?.FindFirst("UserProfileId")?.Value);
        }
        private static Guid GetGuidClaim(string key, HttpContext context)
        {
            var identity = context.User.Identity as ClaimsIdentity;
            return Guid.Parse(identity?.FindFirst("IdentityId")?.Value);
        }
    }
}
