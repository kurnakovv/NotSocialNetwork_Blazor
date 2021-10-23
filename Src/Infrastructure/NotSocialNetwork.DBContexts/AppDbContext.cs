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
            SetRules(modelBuilder);
            SetPublicationAuthor(modelBuilder);
            SetManyToManyForFavorites(modelBuilder);
        }

        private void SetRules(ModelBuilder modelBuilder)
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

        private void SetPublicationAuthor(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PublicationEntity>()
                .HasOne(p => p.Author)
                .WithMany();
        }

        private void SetManyToManyForFavorites(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>()
                .HasMany(p => p.Favorites)
                .WithMany(u => u.Favorites)
                .UsingEntity<FavoritesEntity>(
                        j => j
                            .HasOne(f => f.Publication)
                            .WithMany(p => p.FavoritesEntities)
                            .HasForeignKey(f => f.PublicationId)
                            .OnDelete(DeleteBehavior.Restrict),
                        j => j
                            .HasOne(f => f.User)
                            .WithMany(p => p.FavoritesEntities)
                            .HasForeignKey(f => f.UserId)
                            .OnDelete(DeleteBehavior.Restrict),
                        j =>
                        {
                            j.HasKey(f => new { f.PublicationId, f.UserId });
                        });
        }
    }
}
