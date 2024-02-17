using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tlis.Cms.ImageAssetManagement.Api.Constants;

namespace Tlis.Cms.ImageAssetManagement.Api.Extensions;

public static class AuthorizationSetup
{
    public static void ConfigureAuthorization(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Authority = configuration.GetSection("Jwt").GetValue<string>("Authority");
                options.Audience = configuration.GetSection("Jwt").GetValue<string>("Audience");
                options.SaveToken = true;
            });
        
        services.AddAuthorizationBuilder()
            .AddPolicy(Policy.ImageWrite, policy => policy.RequireClaim("permissions", "write:image"))
            .AddPolicy(Policy.ImageDelete, policy => policy.RequireClaim("permissions", "delete:image"))
            .AddPolicy(Policy.ImageRead, policy => policy.RequireClaim("permissions", "read:image"));
    }
}