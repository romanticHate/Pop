using EvilCorp.Pop.Application.UserProfile.Queries;

namespace EvilCorp.Pop.Api.Registers
{
    public class AutoMapperRgstr:IWebAppBuilderRgstr
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(typeof(Program), typeof(GetAllUserProfilesQry));
            //builder.Services.AddMediatR(typeof(GetAllUserProfiles));
        }
    }
}
