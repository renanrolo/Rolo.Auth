using Rolo.Auth.Core.Data;
using Rolo.Auth.Core.Entities;
using System.Linq;

namespace Rolo.Auth.Core.App
{
    public class AppUsuario : IAppUsuario
    {
        private ContextJwt contextJwt;

        public AppUsuario(ContextJwt contextJwt)
        {
            this.contextJwt = contextJwt;
        }

        public Usuario GetUsuarioByAuthUser(AuthUser authUser)
        {
            return contextJwt.Usuario.SingleOrDefault(x => x.UsuarioId == authUser.UsuarioId);
        }
    }
}
