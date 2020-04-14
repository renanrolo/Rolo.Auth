using System.ComponentModel.DataAnnotations;

namespace Rolo.Auth.Api.ViewModels
{
    public class CadastrarViewModel
    {
        public string Email { get; set; }
        public string Senha { get; set; }
        public string ConfirmacaoSenha { get; set; }
    }
}
