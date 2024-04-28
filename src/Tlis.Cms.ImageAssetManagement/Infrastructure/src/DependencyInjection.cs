using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tlis.Cms.ImageAssetManagement.Infrastructure.Configurations;
using Tlis.Cms.ImageAssetManagement.Infrastructure.Persistence;
using Tlis.Cms.ImageAssetManagement.Infrastructure.Persistence.Interfaces;
using Tlis.Cms.ImageAssetManagement.Infrastructure.Services;
using Tlis.Cms.ImageAssetManagement.Infrastructure.Services.Interfaces;

namespace Tlis.Cms.ImageAssetManagement.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddOptions<ServiceUrlsConfiguration>()
            .Bind(configuration.GetSection("ServiceUrls"))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddSingleton<IStorageService, StorageService>();

        services.AddDbContext(configuration);
        services.AddTransient<IUnitOfWork, UnitOfWork>();
    }

    public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ImageAssetManagementDbContext>(options =>
            {
                options
                    .UseNpgsql(
                        configuration.GetConnectionString("Postgres"),
                        x => x.MigrationsHistoryTable(HistoryRepository.DefaultTableName, ImageAssetManagementDbContext.SCHEMA))
                    .UseSnakeCaseNamingConvention();
            },
            contextLifetime: ServiceLifetime.Transient,
            optionsLifetime: ServiceLifetime.Singleton);
    }
}