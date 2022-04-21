using FluentValidation;
using NotSocialNetwork.Application.Entities;

namespace NotSocialNetwork.EntitiesValidator.User
{
    public class UserValidator : AbstractValidator<UserEntity>
    {
        public UserValidator()
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

            RuleFor(u => u.DateOfBirth)
                .NotNull();

            RuleFor(u => u.Role)
                .NotNull()
                .NotEmpty();
        }
    }
}
