using EvilCorp.Pop.Application.UserProfile.Queries;
using MediatR;

namespace EvilCorp.Pop.Api.Registers
{
    public class MediatrRgstr : IWebAppBuilderRgstr
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {            
            builder.Services.AddMediatR(typeof(GetAllUserProfilesQry));
        }
    }
}
