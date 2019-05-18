using System.ComponentModel.DataAnnotations;

namespace Rolo.Auth.Api.ViewModels
{
    public class CadastrarViewModel
    {
        [Required(ErrorMessage = "É necessário um e-mail válido.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "É necessário informar uma senha.")]
        public string Senha { get; set; }
        [Required(ErrorMessage = "É necessário informar a senha de confirmação.")]
        public string ConfirmacaoSenha { get; set; }
    }
}
