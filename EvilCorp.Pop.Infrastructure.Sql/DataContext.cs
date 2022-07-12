using EvilCorp.Pop.Domain.Aggregates.Post;
using EvilCorp.Pop.Domain.Aggregates.UserProfile;
using EvilCorp.Pop.Infrastructure.Sql.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EvilCorp.Pop.Infrastructure.Sql
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostInteraction> PostInteractions { get;set;}
        public DbSet<PostComment> PostComments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(builder);          
            modelBuilder.ApplyConfiguration(new PostCommentConfig());
            modelBuilder.ApplyConfiguration(new PostInteractionConfig());
            modelBuilder.ApplyConfiguration(new UserProfileConfig());
            modelBuilder.ApplyConfiguration(new IdentityUserLoginConfig());
            modelBuilder.ApplyConfiguration(new IdentityUserRoleConfig());
            modelBuilder.ApplyConfiguration(new IdentityUserTokenConfig());
        }
    }
}
