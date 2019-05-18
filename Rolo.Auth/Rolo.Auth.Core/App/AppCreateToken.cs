using Microsoft.IdentityModel.Tokens;
using Rolo.Auth.Core.Entities;
using Rolo.Auth.Core.TokenConfig;
using Rolo.Auth.Core.ValueObjects;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;


namespace Rolo.Auth.Core.App
{
    public class CreateToken
    {
        public TokenModel Create(AuthUser usuario,
            SigningConfigurations signingConfigurations,
            TokenConfigurations tokenConfigurations)
        {
            ClaimsIdentity identity = new ClaimsIdentity(
                   new GenericIdentity(usuario.Email, "Login"),
                   new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, usuario.Email)
                   }
               );

            DateTime dataCriacao = DateTime.Now;
            DateTime dataExpiracao = dataCriacao + TimeSpan.FromSeconds(tokenConfigurations.Seconds);

            var handler = new JwtSecurityTokenHandler();

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = tokenConfigurations.Issuer,
                Audience = tokenConfigurations.Audience,
                SigningCredentials = signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = dataCriacao,
                Expires = dataExpiracao
            });

            var token = handler.WriteToken(securityToken);


            return new TokenModel
            {
                Created = dataCriacao,
                Expiration = dataExpiracao,
                AccessToken = token,
                Email = usuario.Email
            };
        }
    }
}
