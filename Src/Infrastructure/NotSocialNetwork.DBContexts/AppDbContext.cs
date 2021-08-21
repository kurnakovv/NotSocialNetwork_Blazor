using Microsoft.EntityFrameworkCore;
using NotSocialNetwork.Application.Entities;

namespace NotSocialNetwork.DBContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<PublicationEntity> Publications { get; set; }
        public DbSet<ImageEntity> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>(user =>
            {
                user.Property(u => u.Name)
                    .IsRequired()
                    .HasMaxLength(30);

                user.Property(u => u.Email)
                    .IsRequired();

                user.Property(u => u.DateOfBirth)
                    .IsRequired();

                user.Property(u => u.Role)
                    .IsRequired();
            });

            modelBuilder.Entity<PublicationEntity>(publication =>
            {
                publication.Property(p => p.Text)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<ImageEntity>(image =>
            {
                image.Property(i => i.Title)
                    .IsRequired()
                    .HasMaxLength(100);
            });
        }
    }
}
