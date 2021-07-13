using FluentValidation;
using NotSocialNetwork.Application.DTOs;

namespace NotSocialNetwork.EntitiesValidator.User
{
    public class RegistrationUserDTOValidator : AbstractValidator<RegistrationUserDTO>
    {
        public RegistrationUserDTOValidator()
        {
            SetRules();
        }

        private void SetRules()
        {
            RuleFor(rud => rud.Name)
                .NotNull()
                .NotEmpty()
                .Length(3, 30)
                .WithMessage("Enter valid name.");

            RuleFor(rud => rud.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress();

            RuleFor(rud => rud.DateOfBirth)
                .NotNull();
        }
    }
}
