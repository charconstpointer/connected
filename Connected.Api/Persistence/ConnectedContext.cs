using Connected.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Connected.Api.Persistence
{
    public class ConnectedContext : DbContext
    {
        public ConnectedContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(u => u.HasMany(x => x.CreatedGroups).WithOne(x => x.Creator));
            modelBuilder.Entity<UserGroup>()
                .HasKey(bc => new { bc.UserId, bc.GroupId });
            modelBuilder.Entity<UserGroup>()
                .HasOne(bc => bc.User)
                .WithMany(b => b.Groups)
                .HasForeignKey(bc => bc.UserId);
            modelBuilder.Entity<UserGroup>()
                .HasOne(bc => bc.Group)
                .WithMany(c => c.Users)
                .HasForeignKey(bc => bc.GroupId);
            modelBuilder.Entity<Feed>()
                .Ignore(f => f.Group)
                .HasKey(f => f.Id);

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Feed> Feeds { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Items { get; set; }
    }
}