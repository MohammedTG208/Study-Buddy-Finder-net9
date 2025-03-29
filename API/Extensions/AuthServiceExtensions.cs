using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions;

public static class AuthServiceExtensions
{
    public static IServiceCollection AddAuthServiceExtensions(this IServiceCollection services, IConfiguration config)
    {
        // Add authentication services
        // Add authentication services to the container.
        // This method adds authentication services to the ASP.NET Core dependency injection container.
        // It configures the authentication scheme to use JWT bearer tokens for authentication.
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var token = config["keyToken"] ?? throw new Exception("Key token not found in configuration file");

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(token)),
                    ValidateIssuer = false,
                    ValidateAudience = false,

                };
            });

        return services;
    }
}
