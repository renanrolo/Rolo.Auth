using FluentValidation;
using Rolo.Auth.Core.Entities;

namespace Rolo.Auth.Core.Validations
{
    public class ValidationAuthUser : AbstractValidator<AuthUser>
    {
        public ValidationAuthUser()
        {
            RuleFor(x => x.Email).Must(Validations.NotNullOrEmpty)
                                 .WithMessage("Email não pode ser em branco.")
                                 .EmailAddress()
                                 .WithMessage("Email inválido.");

            RuleFor(x => x.Password).Must(Validations.NotNullOrEmpty)
                                    .WithMessage("Senha não pode ser em branco.")
                                    .Length(4, 128)
                                    .WithMessage("A senha deve ter mais de '03' caracteres.");
        }

    }
}
