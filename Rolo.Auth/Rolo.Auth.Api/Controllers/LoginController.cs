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
            usuario = usuario ?? new AuthUserViewModel();

            var authUser = new Core.Entities.AuthUser() { Email = usuario.Email, Password = usuario.Senha };

            var response = appAuthenticate.ValidanteAndCreateToken(authUser);

            if (!response.Status )
                return BadRequest(response);

            return response;
        }
    }
}