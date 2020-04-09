using Rolo.Auth.Core.Data;
using Rolo.Auth.Core.Entities;
using Rolo.Auth.Core.Validations;
using System.Linq;

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

            if (contextJwt.AuthUser.Any(x => x.Email == authUser.Email))
            {
                return Result<AuthUser>.Error("Já existe um usuario cadastrado com o E-mail informado");
            }

            CriarUsuario(authUser);

            return Result<AuthUser>.Sucess(authUser);
        }

        private void CriarUsuario(AuthUser authUser)
        {
            Usuario usuario = new Usuario()
            {
                Email = authUser.Email
            };

            contextJwt.Usuario.Add(usuario);

            authUser.UsuarioId = usuario.UsuarioId;

            contextJwt.AuthUser.Add(authUser);

            contextJwt.SaveChanges();
        }
    }
}
