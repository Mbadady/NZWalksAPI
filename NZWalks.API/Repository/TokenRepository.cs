using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using NZWalks.API.Models.Domains;

namespace NZWalks.API.Repository
{
	public class TokenRepository : ITokenRepository
	{
        private readonly IConfiguration _configuration;

        public TokenRepository(IConfiguration configuration)
		{
            _configuration = configuration;
        }

        public string CreateJWTToken(ApplicationUser user, List<string> roles)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.Now.AddMinutes(15);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                                            _configuration["Jwt:Audience"],
                                            claims,
                                            expires: expires,
                                            signingCredentials: credentials);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}

