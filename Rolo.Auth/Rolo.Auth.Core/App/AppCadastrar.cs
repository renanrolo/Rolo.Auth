using Rolo.Auth.Core.Data;
using Rolo.Auth.Core.Entities;
using Rolo.Auth.Core.Validations;

namespace Rolo.Auth.Core.App
{
    public class AppCadastrar : IAppCadastrar
    {
        private ContextJwt contextJwt;

        public AppCadastrar(ContextJwt contextJwt)
        {
            this.contextJwt = contextJwt;
        }

        public Result<AuthUser> CadastrarUsuario(AuthUser authUser)
        {
            var validator = new ValidationAuthUser();

            var validatedAuthUser = validator.Validate(authUser);

            if (!validatedAuthUser.IsValid)
                return Result<AuthUser>.Error(validatedAuthUser);

            using (var transaction = contextJwt.Database.BeginTransaction())
            {
                try
                {
                    Usuario usuario = new Usuario();
                    usuario.Nome = authUser.Email;
                    contextJwt.Usuario.Add(usuario);

                    authUser.UsuarioId = usuario.UsuarioId;

                    contextJwt.AuthUser.Add(authUser);
                    contextJwt.SaveChanges();

                    transaction.Commit();
                }
                catch (System.Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            return Result<AuthUser>.Sucess(authUser);
        }
    }
}
