using FluentValidation;
using Khadamat_SellerPortal.Application.OnlineSellers.Common;

namespace Khadamat_SellerPortal.Application.Common.EntitiesValidators
{
    public class PortfolioUrlDtoValidator : AbstractValidator<PortfolioUrlDto>
    {
        public PortfolioUrlDtoValidator()
        {
            //RuleFor(x => x.Type).IsInEnum().WithMessage("Invalid portfolio URL type");

            RuleFor(x => x.Url)
                .NotEmpty().WithMessage("URL is required")
                .MaximumLength(500).WithMessage("URL cannot exceed 500 characters")
                .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _)).WithMessage("URL must be a valid absolute URL");
        }
    }
}
