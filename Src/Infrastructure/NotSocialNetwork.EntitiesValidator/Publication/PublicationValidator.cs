using FluentValidation;
using NotSocialNetwork.Application.Entities;

namespace NotSocialNetwork.EntitiesValidator.Publication
{
    public class PublicationValidator : AbstractValidator<PublicationEntity>
    {
        public PublicationValidator()
        {
            SetRules();
        }

        private void SetRules()
        {
            RuleFor(p => p.Text)
                .NotNull()
                .NotEmpty()
                .Length(3, 100)
                .WithMessage("Invalid text (3 - 100)");

            RuleFor(p => p.Author)
                .NotNull();
        }
    }
}
