using EvilCorp.Pop.Domain.Aggregates.UserProfile;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvilCorp.Pop.Infrastructure.Sql.Configurations
{
    internal class BasicInfoConfig : IEntityTypeConfiguration<BasicInfo>
    {
        public void Configure(EntityTypeBuilder<BasicInfo> builder)
        {
          // owned entity
        }
    }
}
