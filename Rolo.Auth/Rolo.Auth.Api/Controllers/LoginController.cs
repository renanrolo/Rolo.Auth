using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rolo.Auth.Api.ViewModels;
using Rolo.Auth.Core.App;

namespace Rolo.Auth.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAppAuthenticate appAuthenticate;

        public LoginController(IAppAuthenticate appAuthenticate)
        {
            this.appAuthenticate = appAuthenticate;
        }


        [HttpPost]
        [AllowAnonymous]
        public object Post([FromBody]AuthUserViewModel usuario)
        {
            if (!ModelState.IsValid)
                return new { status = false, mensagem = "E-mail e senha são obrigatórios!" };

            return appAuthenticate.ValidanteAndCreateToken(new Core.Entities.AuthUser() { Email = usuario.Email, Password = usuario.Senha });
        }
    }
}