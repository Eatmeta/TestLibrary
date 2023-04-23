using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>((serviceProvider, dbContextOptionsBuilder) =>
        {
            dbContextOptionsBuilder.UseNpgsql(
                serviceProvider.GetRequiredService<IConfiguration>().GetConnectionString("DbConnection"),
                npgsqlDbContextOptionsBuilder =>
                    npgsqlDbContextOptionsBuilder.MigrationsAssembly("Persistence"));
        });
        
        services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
        
        return services;
    }
}


