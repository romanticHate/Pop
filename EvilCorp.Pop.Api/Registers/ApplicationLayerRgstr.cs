using EvilCorp.Pop.Application.Services;

namespace EvilCorp.Pop.Api.Registers
{
    public class ApplicationLayerRgstr : IWebAppBuilderRgstr
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IdentityService>();
        }
    }
}
