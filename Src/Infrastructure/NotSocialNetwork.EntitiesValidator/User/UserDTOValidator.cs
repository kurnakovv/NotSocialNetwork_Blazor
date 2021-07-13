using FluentValidation;
using NotSocialNetwork.Application.DTOs;

namespace NotSocialNetwork.EntitiesValidator.User
{
    public class UserDTOValidator : AbstractValidator<UserDTO>
    {
        public UserDTOValidator()
        {
            SetRules();
        }

        private void SetRules()
        {
            RuleFor(u => u.Name)
                .NotNull()
                .NotEmpty()
                .Length(3, 30)
                .WithMessage("Enter valid name.");

            RuleFor(u => u.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress();
        }
    }
}
