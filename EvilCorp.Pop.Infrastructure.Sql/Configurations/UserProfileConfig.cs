using EvilCorp.Pop.Domain.Aggregates.UserProfile;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvilCorp.Pop.Infrastructure.Sql.Configurations
{
    internal class UserProfileConfig : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            // include basic-info in same table
            builder.OwnsOne(up => up.BasicInfo);
        }
    }
}
