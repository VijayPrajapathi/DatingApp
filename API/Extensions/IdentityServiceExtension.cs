using System;
using System.Text;
using API.Entities;
using CloudinaryDotNet.Actions;
using DatingApp.API.Data;
using DatingApp.API.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions;

public static class IdentityServiceExtension
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
    {

        services.AddIdentityCore<AppUser>(op =>
        {
            op.Password.RequireNonAlphanumeric = false;
            op.Password.RequireUppercase = false;
            op.Password.RequireLowercase = false;
        })
        .AddRoles<AppRole>()
        .AddRoleManager<RoleManager<AppRole>>()
        .AddEntityFrameworkStores<DataContext>()
        ;

        services.AddAuthorizationBuilder().
        AddPolicy("RequireAdminRole", policy =>
        {
            policy.RequireRole("Admin");
        })
        .AddPolicy("RequireModeratorRole", policy =>
        {
            policy.RequireRole("Moderator");
        });

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            var tokenKey = config["TokenKey"] ?? throw new Exception("TokenKey is null");
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
        return services;
    }
}
