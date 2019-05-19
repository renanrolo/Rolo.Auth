using System.ComponentModel.DataAnnotations;

namespace Rolo.Auth.Api.ViewModels
{
    public class AuthUserViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Senha { get; set; }
    }
}
