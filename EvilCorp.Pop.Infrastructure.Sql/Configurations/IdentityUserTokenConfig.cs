using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvilCorp.Pop.Infrastructure.Sql.Configurations
{
    internal class IdentityUserTokenConfig : IEntityTypeConfiguration<IdentityUserToken<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserToken<string>> builder)
        {
            builder.HasNoKey();
        }
    }
}
