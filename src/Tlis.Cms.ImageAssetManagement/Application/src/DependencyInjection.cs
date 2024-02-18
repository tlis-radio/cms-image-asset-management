using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tlis.Cms.ImageAssetManagement.Application.Configurations;
using Tlis.Cms.ImageAssetManagement.Application.Services;
using Tlis.Cms.ImageAssetManagement.Application.Services.Interfaces;

namespace Tlis.Cms.ImageAssetManagement.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddOptions<ImageProcessingConfiguration>()
            .Bind(configuration.GetSection("ImageProcessing"))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        services.AddScoped<IImageProcessingService, ImageProcessingService>();
        services.AddSingleton<IImageService, ImageService>();
    }
}