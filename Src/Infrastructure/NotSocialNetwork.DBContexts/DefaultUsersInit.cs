using NotSocialNetwork.Application.Configs;
using NotSocialNetwork.Application.Entities;
using System.Linq;

namespace NotSocialNetwork.DBContexts
{
    public static class DefaultUsersInit
    {
        private static UserEntity _user = new UserEntity
        {
            Name = "Admin",
            Email = "admin@gmail.com",
            Role = RoleConfig.ADMINISTRATOR,
            Image = new ImageEntity() { Title = "image.jpg"}
        };

        public static void AddAdmin(AppDbContext appDbContext)
        {
            var usersCount = appDbContext.Users.Where(u => u.Email.Contains(_user.Email)).Count();

            if (usersCount == 0)
            {
                appDbContext.Users.Add(_user);
                appDbContext.SaveChanges();
            }
        }
    }
}
