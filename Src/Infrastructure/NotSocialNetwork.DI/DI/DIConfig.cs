using Microsoft.Extensions.DependencyInjection;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Interfaces.Systems;
using NotSocialNetwork.Application.Interfaces.Repositories;
using NotSocialNetwork.Application.Interfaces.Services;
using NotSocialNetwork.Application.Services;
using NotSocialNetwork.Data.EFRepositories;
using NotSocialNetwork.Services.Systems;
using NotSocialNetwork.Services.Facades;
using NotSocialNetwork.Application.Interfaces.Facades;

namespace NotSocialNetwork.DI.DIConfig
{
    public static class DIConfig
    {
        public static void ConfigureDI(this IServiceCollection services)
        {
            ConfigureRepositories(services);
            ConfigureServices(services);
            ConfigureSystems(services);
            ConfigureFacades(services);
        }

        private static void ConfigureRepositories(IServiceCollection services)
        {
            services.AddTransient<IRepository<UserEntity>, EFCoreRepository<UserEntity>>();
            services.AddTransient<IRepository<PublicationEntity>, EFCoreRepository<PublicationEntity>>();
            services.AddTransient<IRepository<ImageEntity>, EFCoreRepository<ImageEntity>>();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPublicationService, PublicationService>();
        }

        private static void ConfigureSystems(IServiceCollection services)
        {
            services.AddTransient<IImageFileSystem, ImageFileSystem>();
            services.AddTransient<IImageRepositorySystem, ImageRepositorySystem>();
            services.AddTransient<IJwtSystem, JwtSystem>();
        }

        private static void ConfigureFacades(IServiceCollection services)
        {
            services.AddTransient<IFileFacade<ImageEntity>, ImageFacade>();
        }
    }
}
