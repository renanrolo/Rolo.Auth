using Rolo.Auth.Core.Data;
using Rolo.Auth.Core.Entities;
using Rolo.Auth.Core.TokenConfig;
using Rolo.Auth.Core.Validations;
using Rolo.Auth.Core.ValueObjects;
using System.Linq;

namespace Rolo.Auth.Core.App
{
    public class AppAuthenticate : IAppAuthenticate
    {
        private SigningConfigurations signingConfigurations;
        private TokenConfigurations tokenConfigurations;
        private ContextJwt contextJwt;

        public AppAuthenticate(SigningConfigurations signingConfigurations,
            TokenConfigurations tokenConfigurations, ContextJwt contextJwt)
        {
            this.signingConfigurations = signingConfigurations;
            this.tokenConfigurations = tokenConfigurations;
            this.contextJwt = contextJwt;
        }

        public Result<TokenModel> ValidanteAndCreateToken(AuthUser authUser)
        {
            var validator = new ValidationAuthUser();

            var validatedAuthUser = validator.Validate(authUser);

            if (!validatedAuthUser.IsValid)
                return Result<TokenModel>.Error(validatedAuthUser);

            var userFromDb = contextJwt.AuthUser.SingleOrDefault(x => x.Email == authUser.Email);

            if (userFromDb == null)
                return Result<TokenModel>.Error("Usuário não localizado.");

            if (userFromDb.Password != authUser.Password)
                return Result<TokenModel>.Error("Senha inválida.");

            var token = new CreateToken().Create(authUser, signingConfigurations, tokenConfigurations);
            return Result<TokenModel>.Sucess(token);
        }
    }
}
