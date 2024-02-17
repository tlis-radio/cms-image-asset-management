using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Tlis.Cms.ImageAssetManagement.Api.Extensions;
using Tlis.Cms.ImageAssetManagement.Application;
using Tlis.Cms.ImageAssetManagement.Infrastructure;

namespace Tlis.Cms.ImageAssetManagement.Api;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddMemoryCache();
        builder.Services
            .AddControllers()
            .AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        builder.Services.ConfigureProblemDetails();
        builder.Services.ConfigureSwagger();
        builder.Services.ConfigureAuthorization(builder.Configuration);

        builder.Services.AddApplication(builder.Configuration);
        builder.Services.AddInfrastructure(builder.Configuration);

        var app = builder.Build();
        
        app.UseExceptionHandler();
        app.UseStatusCodePages();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}