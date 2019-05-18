using Rolo.Auth.Core.Entities;

namespace Rolo.Auth.Core.App
{
    public interface IAppUsuario
    {
        Usuario GetUsuarioByAuthUser(AuthUser authUser);
    }
}
