using EvilCorp.Pop.Api.Options;

namespace EvilCorp.Pop.Api.Registers
{
    public class SwaggerRgstr : IWebAppBuilderRgstr
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen();
            builder.Services.ConfigureOptions<ConfigureSwagger>();
        }
    }
}
