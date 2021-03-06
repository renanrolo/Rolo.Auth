﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rolo.Auth.Api.ViewModels;
using Rolo.Auth.Core.App;
using Rolo.Auth.Core.Entities;

namespace Rolo.Auth.Api.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    [Route("api/[controller]")]
    public class RegisterController : Controller
    {
        private IAppCadastrar cadastrarApp;
        private IAppAuthenticate authenticateApp;

        public RegisterController(IAppCadastrar cadastrarApp, IAppAuthenticate authenticateApp)
        {
            this.cadastrarApp = cadastrarApp;
            this.authenticateApp = authenticateApp;
        }

        [HttpPost]
        [AllowAnonymous]
        public object Post([FromBody]CadastrarViewModel novoLogin)
        {
            novoLogin = novoLogin ?? new CadastrarViewModel();

            var usuarioLoginCriado = cadastrarApp.CadastrarUsuario(new AuthUser()
            {
                Email = novoLogin.Email,
                Password = novoLogin.Senha
            });

            if (!usuarioLoginCriado.Status)
                return BadRequest(usuarioLoginCriado);

            return authenticateApp.ValidanteAndCreateToken(usuarioLoginCriado.Body);
        }
    }
}