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
        public DbSet<PublicationImageEntity> PublicationImages { get; set; }
    }
}
