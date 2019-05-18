using Rolo.Auth.Core.Entities;

namespace Rolo.Auth.Core.App
{
    public interface IAppCadastrar
    {
        Result<AuthUser> CadastrarUsuario(AuthUser authUser);
    }
}
