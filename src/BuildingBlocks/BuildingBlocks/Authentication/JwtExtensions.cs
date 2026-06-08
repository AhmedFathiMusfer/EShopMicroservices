using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Authentication;

public static class JwtExtensions
{
    public static IServiceCollection AddJwtAuthentication(
        this IServiceCollection services,
        IConfiguration configuration,
        string? audience = null)
    {
        var authority = configuration["Jwt:Authority"];
        var resolvedAudience = audience ?? configuration["Jwt:Audience"];

        if (string.IsNullOrWhiteSpace(authority))
        {
            throw new InvalidOperationException("Jwt:Authority configuration is required.");
        }

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = authority;

                if (!string.IsNullOrWhiteSpace(resolvedAudience))
                {
                    options.Audience = resolvedAudience;
                }

                // For local/dev with HTTP and self-signed certs
                options.RequireHttpsMetadata = false;
            });

        services.AddAuthorization();

        return services;
    }
}

