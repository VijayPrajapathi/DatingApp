using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Interfaces;
using DatingApp.API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;

public class TokenService(IConfiguration config, UserManager<AppUser> userManager) : ITokenService
{
    public async Task<string> CreateToken(AppUser user)
    {
         var tokenKey = config.GetValue<string>("TokenKey") ?? throw new Exception("TokenKey is not set in the configuration.");
         if(tokenKey.Length <64) throw new Exception("TokenKey must be at least 64 characters long.");
         var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

         if (user.UserName == null)
         {
             throw new ArgumentException("UserName and UserId must be set for token creation.");
         }

         var claims = new List<Claim>{
            new Claim (ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName)
         };

         var roles = await userManager.GetRolesAsync(user);

      claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

         var tokenDescriptor = new SecurityTokenDescriptor{
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = creds
         };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
         
    }
}
