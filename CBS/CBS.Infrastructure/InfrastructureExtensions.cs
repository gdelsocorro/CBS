using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CBS.Infrastructure;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContextFactory<AppDbContext>(opts =>
            opts.UseSqlServer(connectionString));
        return services;
    }

    public static void InitializeDatabase(this IServiceProvider services)
    {
        var factory = services.GetRequiredService<IDbContextFactory<AppDbContext>>();
        using var db = factory.CreateDbContext();
        DatabaseInitializer.Initialize(db);
    }
}
