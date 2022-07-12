using EvilCorp.Pop.Domain.Aggregates.Post;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvilCorp.Pop.Infrastructure.Sql.Configurations
{
    internal class PostCommentConfig : IEntityTypeConfiguration<PostComment>
    {
        public void Configure(EntityTypeBuilder<PostComment> builder)
        {
            builder.HasKey(pc => pc.CommentId);
        }
    }
}
