using Domain.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Common;

public static class Extensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(MarkerClass));

        var connectionString = configuration.GetConnectionString("App") ??
            throw new InvalidOperationException("Missing connection string. Please specify it under the ConnectionStrings:App config section.");

        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

        return services;
    }
}
