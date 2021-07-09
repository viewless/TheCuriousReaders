using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TheCuriousReaders.DataAccess.Entities;
using TheCuriousReaders.Services.Configurations;
using TheCuriousReaders.Services.Interfaces;

namespace TheCuriousReaders.Services.Services
{
    public class JwtTokenGeneratorService : IJwtTokenGenerator
    {
        private readonly AuthenticationConfiguration _authConfiguration;

        public JwtTokenGeneratorService(AuthenticationConfiguration authConfiguration)
        {
            _authConfiguration = authConfiguration;
        }

        public string GenerateToken(UserEntity user, IList<string> roles)
        {
            //make a list with role claims with all roles
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
            {
                roleClaims.Add(new Claim("roles", role));
            }

            //create and encrypt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_authConfiguration.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            tokenDescriptor.Subject.AddClaims(roleClaims);

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encryptedToken = tokenHandler.WriteToken(token);

            return encryptedToken;
        }
    }
}
