using Rolo.Auth.Core.App;
using Rolo.Auth.Core.Data;
using Rolo.Auth.Core.Entities;
using Rolo.Auth.Core.TokenConfig;
using System.Collections.Generic;

namespace Rolo.Auth.Tests.Builders
{
    class BuilderAppAuthenticate
    {
        private List<AuthUser> authUsers;
        private BuilderAppAuthenticate()
        {
            this.authUsers = new List<AuthUser>();
        }

        public static BuilderAppAuthenticate New()
        {
            return new BuilderAppAuthenticate();
        }

        public IAppAuthenticate Build()
        {
            SigningConfigurations signingConfigurations = new SigningConfigurations();
            TokenConfigurations tokenConfigurations = new TokenConfigurations();

            ContextJwt contextJwt = BuilderJwtContext.New().With(authUsers).Build();

            return new AppAuthenticate(signingConfigurations, tokenConfigurations, contextJwt);
        }

        internal BuilderAppAuthenticate Com(AuthUser authUser)
        {
            this.authUsers.Add(authUser);
            return this;
        }
    }
}
