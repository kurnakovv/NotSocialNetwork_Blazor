using FluentValidation;
using NotSocialNetwork.Application.DTOs;

namespace NotSocialNetwork.EntitiesValidator.Publication
{
    public class PublicationDTOValidator : AbstractValidator<PublicationDTO>
    {
        public PublicationDTOValidator()
        {
            SetRules();
        }

        private void SetRules()
        {
            RuleFor(apd => apd.Text)
                .NotNull()
                .NotEmpty()
                .Length(3, 100)
                .WithMessage("Invalid text (3 - 100)");
        }
    }
}
