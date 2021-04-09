using Microsoft.Extensions.DependencyInjection;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Interfaces.Managers;
using NotSocialNetwork.Application.Interfaces.Repositories;
using NotSocialNetwork.Application.Interfaces.Services;
using NotSocialNetwork.Application.Services;
using NotSocialNetwork.Data.EFRepositories;
using NotSocialNetwork.Services.Managers;

namespace NotSocialNetwork.DI.DIConfig
{
    public static class DIConfig
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddTransient<IRepository<UserEntity>, EFCoreRepository<UserEntity>>();
            services.AddTransient<IRepository<PublicationEntity>, EFCoreRepository<PublicationEntity>>();
            services.AddTransient<IRepository<ImageEntity>, EFCoreRepository<ImageEntity>>();

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPublicationService, PublicationService>();

            services.AddTransient<IFileSystem<ImageEntity>, ImageFileSystem>();
        }
    }
}
