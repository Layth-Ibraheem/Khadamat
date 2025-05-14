using FluentValidation;
using Khadamat_SellerPortal.Application.OnlineSellers.Common;

namespace Khadamat_SellerPortal.Application.Common.EntitiesValidators
{
    public class CertificateDtoValidator : AbstractValidator<CertificateDto>
    {
        public CertificateDtoValidator()
        {
            RuleFor(x => x.FilePath)
                .NotEmpty().WithMessage("File path is required")
                .MaximumLength(1000).WithMessage("File path cannot exceed 1000 characters");

            RuleFor(x => x.Description)
                .MaximumLength(250).WithMessage("Description cannot exceed 250 characters")
                .When(x => !string.IsNullOrEmpty(x.Description));
        }
    }
}
