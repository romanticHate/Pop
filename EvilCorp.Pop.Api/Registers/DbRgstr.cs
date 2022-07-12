using EvilCorp.Pop.Infrastructure.Sql;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EvilCorp.Pop.Api.Registers
{
    public class DbRgstr:IWebAppBuilderRgstr
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {           
            var connStr = builder.Configuration.GetConnectionString("Default");
            builder.Services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(connStr);
            });

            builder.Services.AddIdentityCore<IdentityUser>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;

            }).AddEntityFrameworkStores<DataContext>();
            //.AddDefaultTokenProviders();
        }
    }
}
