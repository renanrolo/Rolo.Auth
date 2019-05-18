using Rolo.Auth.Core.App;
using Rolo.Auth.Core.Data;

namespace Rolo.Auth.Tests.Builders
{
    public class BuilderCadastrarApp
    {
        private ContextJwt jwtContext;

        private BuilderCadastrarApp()
        {

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
                                              .Build();

            return new AppCadastrar(jwtContext);
        }

    }
}
