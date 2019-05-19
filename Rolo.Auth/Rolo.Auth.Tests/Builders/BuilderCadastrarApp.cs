using System;
using System.Collections.Generic;
using Rolo.Auth.Core.App;
using Rolo.Auth.Core.Data;
using Rolo.Auth.Core.Entities;

namespace Rolo.Auth.Tests.Builders
{
    public class BuilderCadastrarApp
    {
        private ContextJwt jwtContext;

        private List<AuthUser> authUsers;


        private BuilderCadastrarApp()
        {
            authUsers = new List<AuthUser>();
        }

        public static BuilderCadastrarApp New()
        {
            return new BuilderCadastrarApp();
        }

        public static BuilderCadastrarApp New(ContextJwt jwtContext)
        {
            return new BuilderCadastrarApp() { jwtContext = jwtContext };
        }

        public IAppCadastrar Build()
        {
            if (jwtContext == null)
                jwtContext = BuilderJwtContext.New()
                                              .With(authUsers)
                                              .Build();

            return new AppCadastrar(jwtContext);
        }

        internal BuilderCadastrarApp With(AuthUser authUser)
        {
            authUsers.Add(authUser);
            return this;
        }
    }
}
