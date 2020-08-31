﻿using Microsoft.IdentityModel.Tokens;
using SkyCoApi.Models.DTO.Single;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Web;

namespace SkyCoApi.Controllers.Token
{
    /// <summary>
    /// JWT Token generator class using "secret-key"
    /// more info: https://self-issued.info/docs/draft-ietf-oauth-json-web-token.html
    /// </summary>
    internal static class TokenGenerator
    {
        public static string GenerateTokenJwt(Skyco_AccountDTO account)
        {
            //protect correctly this settings
            String secretKey = ConfigurationManager.AppSettings["JWT_SECRET_KEY"];
            String audienceToken = ConfigurationManager.AppSettings["JWT_AUDIENCE_TOKEN"];
            String issuerToken = ConfigurationManager.AppSettings["JWT_ISSUER_TOKEN"];
            String expireTime = ConfigurationManager.AppSettings["JWT_EXPIRE_MINUTES"];

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(secretKey));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[] {
                new Claim("Idaccount", account.AccountId.ToString()),
                new Claim("UserId", account.UserId.ToString()),
                new Claim("username", account.Username),
                new Claim("UserRole", account.Role),
                new Claim("PhoneNumber", account.PhoneNumber),
                new Claim("EmailAddress", account.EmailAddress),
                new Claim("refreshtoken", account.refreshtoken),
            });

            // create token to the user 
            System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            System.IdentityModel.Tokens.Jwt.JwtSecurityToken jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
                audience: audienceToken,
                issuer: issuerToken,
                subject: claimsIdentity,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(expireTime)),
                signingCredentials: signingCredentials);
            String jwtTokenString = tokenHandler.WriteToken(jwtSecurityToken);
            //var rd = tokenHandler.ReadJwtToken(jwtTokenString);
            return jwtTokenString;
        }

        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        public static string GenerateToken(ClaimsIdentity identityClaims, String newrefresh)
        {
            Skyco_AccountDTO mdl = new Skyco_AccountDTO()
            {
                AccountId = Convert.ToInt64(identityClaims.FindFirst("Idaccount").Value),
                Username = identityClaims.FindFirst("username").Value,
                PhoneNumber = identityClaims.FindFirst("PhoneNumber").Value,
                UserId = Convert.ToInt64(identityClaims.FindFirst("UserId").Value),
                Role = identityClaims.FindFirst("Role").Value,
                refreshtoken = newrefresh,
                EmailAddress = identityClaims.FindFirst("EmailAddress").Value,
            };
            String token = TokenGenerator.GenerateTokenJwt(mdl);

            return token;
        }
    }
}