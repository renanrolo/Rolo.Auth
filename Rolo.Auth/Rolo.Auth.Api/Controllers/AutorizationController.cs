using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rolo.Auth.Api.ViewModels;
using Rolo.Auth.Core.App;
using Rolo.Auth.Core.Entities;

namespace Rolo.Auth.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AutorizationController : ControllerBase
    {
        private IAppAuthenticate appAuthenticate;

        public AutorizationController(IAppAuthenticate appAuthenticate)
        {
            this.appAuthenticate = appAuthenticate;
        }

        [HttpPost]
        [AllowAnonymous]
        public object Post([FromBody]AuthUserViewModel login)
        {
            if (login == null)
                return Result<AuthUserViewModel>.Error("Informe um usuário e senha");

            return appAuthenticate.ValidanteAndCreateToken(new AuthUser()
            {
                Email = login.Email,
                Password = login.Senha
            });
        }
    }
}