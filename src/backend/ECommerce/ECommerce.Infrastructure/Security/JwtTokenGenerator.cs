using ECommerce.Application.Common.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using ECommerce.Application.Common.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ECommerce.Infrastructure.Security
{
    public class JwtTokenGenerator : ITokenGenerator
    {
        private readonly IConfigurationSection _jwtSettings;

        public JwtTokenGenerator(IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            _jwtSettings = jwtSettings;
        }

        public TokenResult GenerateToken(User user)
        {
            var accessExpiry = DateTime.UtcNow.AddMinutes(double.Parse(_jwtSettings["AccessTokenExpirationMinutes"] ?? "15"));
            var RefreshExpiry = DateTime.UtcNow.AddDays(double.Parse(_jwtSettings["RefreshTokenExpirationDays"] ?? "7"));
            var accessToken = GenerateAccessToken(user, accessExpiry, _jwtSettings);
            var refreshToken = GenerateRefreshToken();

            return new TokenResult
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                AccessTokenExpiration = accessExpiry,
                RefreshTokenExpiration = RefreshExpiry
            };
        }

        private string GenerateAccessToken(User user, DateTime accessExpiry, IConfigurationSection jwtSettings)
        {
            var secrectKey = jwtSettings["SecretKey"] ?? throw new Exception("JWT SecretKey is not configured.");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secrectKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new Dictionary<string, object>
            {
                [ClaimTypes.NameIdentifier] = user.Id.ToString(),
                [ClaimTypes.Name] = user.FullName,
                [ClaimTypes.Email] = user.Email,
                [ClaimTypes.Role] = user.Role.ToString(),
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"],
                Claims = claims,
                Expires = accessExpiry,
                SigningCredentials = creds,
            };

            var tokenhandler = new JsonWebTokenHandler();
            var token = tokenhandler.CreateToken(tokenDescriptor);

            return token;
        }

        private string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
