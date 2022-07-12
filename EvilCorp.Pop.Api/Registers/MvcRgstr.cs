using EvilCorp.Pop.Api.Filters;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace EvilCorp.Pop.Api.Registers
{
    public class MvcRgstr : IWebAppBuilderRgstr
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers( config =>
            {
                // Filter apply globally in all controllers 
                config.Filters.Add(typeof(PopExceptionHandler));               
            });

            builder.Services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
                config.ApiVersionReader = new UrlSegmentApiVersionReader();
            });

            builder.Services.AddVersionedApiExplorer(config => {
                config.GroupNameFormat = "'v'VVV";
                config.SubstituteApiVersionInUrl = true;
            });



            builder.Services.AddEndpointsApiExplorer();
        }
    }
}
