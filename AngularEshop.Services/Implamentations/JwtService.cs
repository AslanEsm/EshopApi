using AngularEshop.Common;
using AngularEshop.Entities.Account;
using AngularEshop.Services.Intefaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AngularEshop.Services.Implamentations
{
    public class JwtService : IJwtService
    {
        private readonly SiteSettings _siteSetting;
        private readonly SignInManager<User> signInManager;

        public JwtService(
            IOptionsSnapshot<SiteSettings> settings,
            SignInManager<User> signInManager
      )
        {
            _siteSetting = settings.Value;
            this.signInManager = signInManager;
        }

        public async Task<UserToken> GenerateAsync(User user)
        {
            var secretKey = Encoding.UTF8.GetBytes(_siteSetting.JwtSettings.SecretKey);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

            var encryptionkey = Encoding.UTF8.GetBytes(_siteSetting.JwtSettings.Encryptkey);
            var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encryptionkey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);
            var expirationDate = DateTime.Now.AddMinutes(_siteSetting.JwtSettings.ExpirationMinutes);
            var claims = await _getClaimsAsync(user);

            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = _siteSetting.JwtSettings.Issuer,
                Audience = _siteSetting.JwtSettings.Audience,
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now.AddMinutes(_siteSetting.JwtSettings.NotBeforeMinutes),
                Expires = expirationDate,
                SigningCredentials = signingCredentials,
                EncryptingCredentials = encryptingCredentials,
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateToken(descriptor);

            var jwt = tokenHandler.WriteToken(securityToken);

            return new UserToken() { Token = jwt, Expiration = expirationDate };
        }

        private async Task<IEnumerable<Claim>> _getClaimsAsync(User user)
        {
            var result = await signInManager.ClaimsFactory.CreateAsync(user);
            var list = new List<Claim>(result.Claims);
            return list;
        }
    }
}