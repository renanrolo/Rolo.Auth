using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rolo.Auth.Core.App;
using Rolo.Auth.Core.Entities;
using Rolo.Auth.Tests.Builders;

namespace Rolo.Auth.Tests.Core
{
    [TestClass]
    public class TestesAuhentications
    {
        public const string validEmail = "rolo@rolo.com.vc";
        public const string validPassword = "1234";

        [TestMethod]
        public void Testar_ValidanteAndCreateToken_com_usuario_valido()
        {
            IAppAuthenticate appAuthenticate = BuilderAppAuthenticate.New()
                                                                     .Com(new AuthUser()
                                                                     {
                                                                         Email = validEmail,
                                                                         Password = validPassword
                                                                     })
                                                                     .Build();

            var authUser = new AuthUser() { Email = validEmail, Password = validPassword };

            var resultAuthUser = appAuthenticate.ValidanteAndCreateToken(authUser);

            Assert.IsTrue(resultAuthUser.Status, "Erro no result");
        }
    }
}
