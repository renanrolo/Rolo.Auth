using System.ComponentModel.DataAnnotations;

namespace Rolo.Auth.Api.ViewModels
{
    public class AuthUserViewModel
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
