using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rolo.Auth.Core.App;
using Rolo.Auth.Core.Data;
using Rolo.Auth.Core.Entities;
using Rolo.Auth.Tests.Builders;
using System;
using System.Linq;

namespace Rolo.Auth.Tests.Core
{
    [TestClass]
    public class TestesCadastrar
    {
        public const string validEmail = "rolo@rolo.com.vc";
        public const string validPassword = "1234";

        [TestMethod]
        public void Testar_cadastro_invalido_usuario_vazio()
        {
            IAppCadastrar cadastrarApp = BuilderCadastrarApp.New()
                                                          .Build();

            var user = new AuthUser()
            {
                Email = "",
                Password = ""
            };

            var newUserReult = cadastrarApp.CadastrarUsuario(user);

            Assert.IsFalse(newUserReult.Status);
            Assert.IsTrue(newUserReult.IfMsgExist("Email não pode ser em branco."));
            Assert.IsTrue(newUserReult.IfMsgExist("Senha não pode ser em branco."));
        }

        [TestMethod]
        public void Testar_cadastro_invalido_usuario_com_email_vazio()
        {
            IAppCadastrar cadastrarApp = BuilderCadastrarApp.New()
                                                            .Build();

            var user = new AuthUser()
            {
                Email = "",
                Password = validPassword
            };

            var newUserResult = cadastrarApp.CadastrarUsuario(user);

            Assert.IsFalse(newUserResult.Status, "Erro no Result");
            Assert.IsTrue(newUserResult.OnlyMsg("Email não pode ser em branco."));
        }

        [TestMethod]
        [DataRow("rolo")]
        [DataRow("rolo@rolo")]
        [DataRow("@rolo.com")]
        [DataRow("@rolo.com")]
        [DataRow("rolo@.com")]
        public void Testar_cadastro_invalido_usuario_com_email_em_formato_invalido(string emailInvalido)
        {
            IAppCadastrar cadastrarApp = BuilderCadastrarApp.New()
                                                          .Build();

            var user = new AuthUser()
            {
                Email = emailInvalido,
                Password = validPassword
            };

            var newUserResult = cadastrarApp.CadastrarUsuario(user);

            Assert.IsFalse(newUserResult.Status, "Erro no Result");
            Assert.IsTrue(newUserResult.OnlyMsg("Email inválido."), "Erro em 'Email inválido.'");
        }

        [TestMethod]
        public void Testar_cadastro_invalido_usuario_com_senha_vazia()
        {
            IAppCadastrar cadastrarApp = BuilderCadastrarApp.New()
                                                          .Build();

            var user = new AuthUser()
            {
                Email = validEmail,
                Password = ""
            };

            var newUserReult = cadastrarApp.CadastrarUsuario(user);

            Assert.IsFalse(newUserReult.Status, "Erro no Result");
            Assert.IsTrue(newUserReult.OnlyMsg("Senha não pode ser em branco."));
        }

        [TestMethod]
        public void Testar_cadastro_invalido_usuario_com_senha_pequena()
        {
            IAppCadastrar cadastrarApp = BuilderCadastrarApp.New()
                                                            .Build();

            var user = new AuthUser()
            {
                Email = validEmail,
                Password = "123"
            };

            var newUserReult = cadastrarApp.CadastrarUsuario(user);

            Assert.IsFalse(newUserReult.Status, "Erro no Result");
            Assert.IsTrue(newUserReult.OnlyMsg("A senha deve ter mais de '03' caracteres."), "Validação de mensagem única falhou.");
        }

        [TestMethod]
        public void Testar_cadastro_invalido_email_em_uso_por_outro_usuario()
        {
            IAppCadastrar cadastrarApp = BuilderCadastrarApp.New()
                                                            .With(new AuthUser() { Email = validEmail })
                                                            .Build();

            var user = new AuthUser()
            {
                Email = validEmail,
                Password = validPassword
            };

            var newUserReult = cadastrarApp.CadastrarUsuario(user);

            Assert.IsFalse(newUserReult.Status, "Erro no Result");
            Assert.IsTrue(newUserReult.OnlyMsg("Já existe um usuario cadastrado com o E-mail informado"), "Validação de mensagem única falhou.");
        }

        [TestMethod]
        public void Testar_cadastrar_usuario_com_sucesso()
        {
            //Arrange
            ContextJwt jwtContext = BuilderJwtContext.New().Build();

            IAppCadastrar cadastrarApp = BuilderCadastrarApp.New(jwtContext)
                                                          .Build();

            IAppUsuario usuarioApp = BuilderUsuarioApp.New(jwtContext)
                                                      .Build();

            var user = new AuthUser()
            {
                Email = validEmail,
                Password = validPassword
            };

            //Act
            Result<AuthUser> resultNewUser = cadastrarApp.CadastrarUsuario(user);

            //Assert
            Assert.IsTrue(resultNewUser.Status, "AuthUser não é válido");
            Assert.IsTrue(resultNewUser.Body.AuthUserId > 0, "AuthUser não foi criado");

            //Ao criar 'AuthUser' deve ser criado automaticamente um 'Usuario'
            Usuario newUsuario = usuarioApp.GetUsuarioByAuthUser(resultNewUser.Body);
            Assert.IsNotNull(newUsuario, "Usuario não foi criado");
            Assert.AreEqual(validEmail, newUsuario.Nome, "Usuario não foi criado com o email informado");
        }
    }


    public static class CadastrarAppExtendedForTests
    {
        public static Boolean IfMsgExist(this Result<AuthUser> result, string errorMessage)
        {
            return result.Errors.Any(x => x == errorMessage);
        }

        public static Boolean OnlyMsg(this Result<AuthUser> result, string errorMessage)
        {
            return result.Errors.Count(x => x == errorMessage) == 1;
        }
    }
}
