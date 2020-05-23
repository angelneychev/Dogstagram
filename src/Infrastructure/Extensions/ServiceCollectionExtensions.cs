namespace Dogstagram.Infrastructure.Extensions
{
    using System.Text;

    using Dogstagram.Data;
    using Dogstagram.Data.Model;
    using Dogstagram.Features.Dogs;
    using Dogstagram.Features.Identity;
    using Dogstagram.Infrastructure.Filters;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;

    public static class ServiceCollectionExtensions
    {
      public static AppSettings GetApplicationSettings(this IServiceCollection services, IConfiguration configuration)
      {
        var applicationSettingsConfiguration = configuration.GetSection("ApplicationSettings");
        services.Configure<AppSettings>(applicationSettingsConfiguration);
        return applicationSettingsConfiguration.Get<AppSettings>();
      }

      public static IServiceCollection AddDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
        => services.AddDbContext<DogstagramDbContext>(
          options =>
            options.UseSqlServer(
              configuration
              .GetDefaultConnectionString()));

      public static IServiceCollection AddIdentity(this IServiceCollection services)
      {
        services.AddIdentity<User, IdentityRole>(
            option =>
            {
              option.Password.RequireDigit = false;
              option.Password.RequireLowercase = false;
              option.Password.RequireNonAlphanumeric = false;
              option.Password.RequireUppercase = false;
              option.Password.RequiredLength = 6;
            })
          .AddEntityFrameworkStores<DogstagramDbContext>();

        return services;
      }

      public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, AppSettings appSettings)
      {
        var key = Encoding.ASCII.GetBytes(appSettings.Secret);

        services.AddAuthentication(
            x =>
            {
              x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
              x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
          .AddJwtBearer(
            x =>
            {
              x.RequireHttpsMetadata = false;
              x.SaveToken = true;
              x.TokenValidationParameters = new TokenValidationParameters
              {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
              };
            });
        return services;
      }

      public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        => services
          .AddTransient<IIdentityService, IdentityService>()
          .AddTransient<IDogsService, DogsService>();

      public static IServiceCollection AddSwagger(this IServiceCollection services)
      => services.AddSwaggerGen(
          c =>
            c.SwaggerDoc(
              "v1",
              new OpenApiInfo
              {
                Title = "My Dogstagram API",
                Version = "v1",
              }));

      public static void AddApiController(this IServiceCollection service)
      => service
        .AddControllers(options => options
          .Filters
          .Add<ModelOrNotFoundActionFilter>());
  }
}
