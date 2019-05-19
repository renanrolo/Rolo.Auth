using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rolo.Auth.Api.ViewModels;
using Rolo.Auth.Core.App;

namespace Rolo.Auth.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IAppAuthenticate authenticateApp;

        public LoginController(IAppAuthenticate authenticateApp)
        {
            this.authenticateApp = authenticateApp;
        }


        [HttpPost]
        [AllowAnonymous]
        public object Post([FromBody]AuthUserViewModel usuario)
        {
            if (!ModelState.IsValid)
                return new { status = false, mensagem = "E-mail e senha são obrigatórios!" };

            return authenticateApp.ValidanteAndCreateToken(new Core.Entities.AuthUser() { Email = usuario.Email, Password = usuario.Senha });
        }
    }
}