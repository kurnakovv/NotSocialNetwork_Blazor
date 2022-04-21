using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NotSocialNetwork.Application.Configs;
using NotSocialNetwork.DBContexts;
using NotSocialNetwork.DI.DIConfig;
using NotSocialNetwork.Mapping.AutoMapper;
using NotSocialNetwork.WebShared.Helpers;
using System.Collections.Generic;
using System.Text;
using FluentValidation.AspNetCore;

namespace NotSocialNetwork.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private const string CONFIG_NAME = "_myAllowSpecificOriginss";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: CONFIG_NAME,
                                  builder =>
                                  {
                                      builder.WithOrigins(HttpHelper.API_ADDRESS);
                                      builder.AllowAnyMethod();
                                      builder.AllowAnyHeader();
                                      builder.AllowAnyOrigin();
                                  });
            });

            services.AddControllers();
            services.AddMvc(setup => { }).AddFluentValidation();
            services.ConfigureDI();

            // In-memory database.
            //ConfigureInMemoryDatabase(services);
            // Real database.
            ConfigureProductionServices(services);
            // Real database for docker.
            //ConfigureProductionServicesForDocker(services);

            ConnectMapping(services);
            ConnectSwagger(services);
            ConnectJwt(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NotSocialNetwork.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(CONFIG_NAME);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigureInMemoryDatabase(IServiceCollection services)
        {
            SetupDbContextInStartup.AddMemoryDbContext(services, "MemoryDatabase");
        }

        private void ConfigureProductionServices(IServiceCollection services)
        {
            SetupDbContextInStartup.AddDbContext(services, Configuration.GetConnectionString("NotSocialNetworkDB"));
        }

        private void ConfigureProductionServicesForDocker(IServiceCollection services)
        {
            SetupDbContextInStartup.AddDbContext(services, Configuration.GetValue<string>("PersistenceModule:DefaultConnection"));
        }

        private void ConnectMapping(IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutomapperConfig());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        private void ConnectSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NotSocialNetwork.API", Version = "v1" });
                c.EnableAnnotations();

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n" +
                        "Enter 'Bearer' [space] and then your token in the text input below." +
                        "\r\n\r\nExample: 'Bearer 12345abcdef'",

                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                            Scheme = "oatuh2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
            });
        }
        
        private void ConnectJwt(IServiceCollection services)
        {
            var key = Encoding.ASCII.GetBytes(JwtConfig.SECRET);
            services.AddAuthentication(config =>
            {
                config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(config =>
            {
                config.RequireHttpsMetadata = false;
                config.SaveToken = true;
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }
    }
}
