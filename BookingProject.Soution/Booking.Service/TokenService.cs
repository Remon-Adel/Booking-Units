using Booking.Core.Identity;
using Booking.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Service
{
    public class TokenService:ITokenService
    {
        public TokenService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public async Task<string> CreateToken(AppUser user, UserManager<AppUser> userManager)
        {
            // private claims (user defined)
            var authclaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName, user.DisplayName)
            };
            var userRoles = await userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
                authclaims.Add(new Claim(ClaimTypes.Role, role));

            var authKeys = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"]));

            var token = new JwtSecurityToken(

                issuer: Configuration["JWT:ValidIssuer"],
                audience: Configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(double.Parse(Configuration["JWT:DurationInDays"])),
                claims: authclaims,
                signingCredentials: new SigningCredentials(authKeys, SecurityAlgorithms.HmacSha256Signature)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);


        }
    }
}
