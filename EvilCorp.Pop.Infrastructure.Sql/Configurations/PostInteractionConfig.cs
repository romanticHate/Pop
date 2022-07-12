using EvilCorp.Pop.Domain.Aggregates.Post;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvilCorp.Pop.Infrastructure.Sql.Configurations
{
    internal class PostInteractionConfig : IEntityTypeConfiguration<PostInteraction>
    {
        public void Configure(EntityTypeBuilder<PostInteraction> builder)
        {
            builder.HasKey(pi => pi.InteractionId);
        }
    }
}
