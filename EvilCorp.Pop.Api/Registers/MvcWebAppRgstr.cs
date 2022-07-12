using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace EvilCorp.Pop.Api.Registers
{
    public class MvcWebAppRgstr : IWebAppRgstr
    {
        public void RegisterPipelineComponents(WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                        description.ApiVersion.ToString());
                }
            });

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();
        }
    }
}
