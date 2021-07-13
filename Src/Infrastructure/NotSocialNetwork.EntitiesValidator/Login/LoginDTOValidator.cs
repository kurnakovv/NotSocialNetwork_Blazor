using FluentValidation;
using NotSocialNetwork.Application.DTOs;

namespace NotSocialNetwork.EntitiesValidator.Login
{
    public class LoginDTOValidator : AbstractValidator<LoginDTO>
    {
        public LoginDTOValidator()
        {
            SetRules();
        }

        private void SetRules()
        {
            RuleFor(ld => ld.Email)
                .EmailAddress();
        }
    }
}
