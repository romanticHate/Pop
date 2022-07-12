using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvilCorp.Pop.Infrastructure.Sql.Configurations
{
    internal class IdentityUserLoginConfig : IEntityTypeConfiguration<IdentityUserLogin<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserLogin<string>> builder)
        {
            builder.HasKey(iul => iul.UserId);
        }
    }
}
