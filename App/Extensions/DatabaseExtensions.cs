using Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Api.Extensions
{
    public static class DatabaseExtensions
    {
        public static IApplicationBuilder MigrateDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<DatabaseContext>();
            if (context is { Database: not null })
            {
                context.Database.Migrate();
            }

            return app;
        }
    }
}
