namespace Dogstagram.Infrastructure.Extensions
{
    using Dogstagram.Data;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseSwaggerUI(this IApplicationBuilder app)
        => app
            .UseSwagger()
            .UseSwaggerUI(
              options =>
              {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "My Dogstagram API");
                options.RoutePrefix = string.Empty;
              });

        public static void ApplyMigration(this IApplicationBuilder app)
        {
            using var services = app.ApplicationServices.CreateScope();

            var dbContext = services.ServiceProvider.GetService<DogstagramDbContext>();

            dbContext.Database.Migrate();
        }
    }
}
