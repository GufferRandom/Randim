using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Randim.Common.Shared.Extensions;

public static class ValidateJwtToken
{
    public static void AddJwtTokenValidator(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var publicKeyPem = Environment.GetEnvironmentVariable("PUBLIC_KEY_Keycloack");
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.MapInboundClaims = false;
                options.MetadataAddress =
                    "http://localhost:8080/realms/RandimSocialMedia/.well-known/openid-configuration";
                options.Authority = "http://localhost:8080/realms/RandimSocialMedia";
                options.Audience = "account";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                };
            });
    }
}
