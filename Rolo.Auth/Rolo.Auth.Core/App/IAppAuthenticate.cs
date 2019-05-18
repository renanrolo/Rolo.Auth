using Rolo.Auth.Core.Entities;
using Rolo.Auth.Core.ValueObjects;

namespace Rolo.Auth.Core.App
{
    public interface IAppAuthenticate
    {
        Result<TokenModel> ValidanteAndCreateToken(AuthUser authUser);
    }
}
