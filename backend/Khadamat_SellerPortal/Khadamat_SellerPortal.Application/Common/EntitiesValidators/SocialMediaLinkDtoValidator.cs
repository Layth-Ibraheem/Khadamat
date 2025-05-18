using FluentValidation;
using Khadamat_SellerPortal.Application.OnlineSellers.Common;

namespace Khadamat_SellerPortal.Application.Common.EntitiesValidators
{
    public class SocialMediaLinkDtoValidator : AbstractValidator<SocialMediaLinkDto>
    {
        public SocialMediaLinkDtoValidator()
        {
            //RuleFor(x => x.Type).IsInEnum().WithMessage("Invalid social media type");

            RuleFor(x => x.Link)
                .NotEmpty().WithMessage("Link is required")
                .MaximumLength(500).WithMessage("Link cannot exceed 500 characters")
                .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _)).WithMessage("Link must be a valid absolute URL");
        }
    }
}
