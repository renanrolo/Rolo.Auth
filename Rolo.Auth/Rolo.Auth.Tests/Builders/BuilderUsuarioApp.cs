using Rolo.Auth.Core.App;
using Rolo.Auth.Core.Data;

namespace Rolo.Auth.Tests.Builders
{
    class BuilderUsuarioApp
    {
        private ContextJwt contextJwt;

        private BuilderUsuarioApp()
        {

        }


        public static BuilderUsuarioApp New()
        {
            return new BuilderUsuarioApp();
        }

        public static BuilderUsuarioApp New(ContextJwt contextJwt)
        {
            return new BuilderUsuarioApp() { contextJwt = contextJwt };
        }


        public IAppUsuario Build()
        {
            if (contextJwt == null)
                contextJwt = BuilderJwtContext.New().Build();

            return new AppUsuario(contextJwt);
        }

    }
}
