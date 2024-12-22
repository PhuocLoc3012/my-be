using Application.Dtos.AuthDto;
using Application.IServices;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.JwtFeatures
{
    public class JwtHandler: IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _jwtSettings;
        public JwtHandler(IConfiguration configuration)
        {
            _configuration = configuration;
            _jwtSettings = _configuration.GetSection("JwtSettings");
        }


        public  TokenDto CreateToken(ApplicationUser user, IList<string> roles, bool populateExp)
        {
           
            var signingCredentials = GetSigningCredentials();
            var claims = GetClaims(user, roles);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            var refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            if (populateExp)
            {
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(Convert.ToDouble(_jwtSettings["RefreshTokenExpirationDays"]));
            }
            
            var accessToken =  new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return new TokenDto(accessToken, refreshToken);
        }
        



        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_jwtSettings["SecretKey"]);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private List<Claim> GetClaims(ApplicationUser user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };
            foreach (var role in roles) {
                claims.Add(new Claim(ClaimTypes.Role, role));
            } 
            return claims;
        } 


        public JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken(
                issuer: _jwtSettings["Issuer"],
                audience: _jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings["AccessTokenExpirationMinutes"])),
                signingCredentials: signingCredentials
                );
            return tokenOptions;    
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtSettings["Issuer"],
                ValidAudience = _jwtSettings["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings["SecretKey"])),
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters,out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken is null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid Token");
            }
            return principal;
        }

    }
}
