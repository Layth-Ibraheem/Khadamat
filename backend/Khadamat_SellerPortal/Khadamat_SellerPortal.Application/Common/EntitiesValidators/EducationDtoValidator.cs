using FluentValidation;
using Khadamat_SellerPortal.Application.OnlineSellers.Common;

namespace Khadamat_SellerPortal.Application.Common.EntitiesValidators
{
    public class EducationDtoValidator : AbstractValidator<EducationDto>
    {
        public EducationDtoValidator()
        {
            RuleFor(x => x.Institution)
                .NotEmpty().WithMessage("Institution is required")
                .MaximumLength(150).WithMessage("Institution name cannot exceed 150 characters");

            RuleFor(x => x.FieldOfStudy)
                .NotEmpty().WithMessage("Field of study is required")
                .MaximumLength(150).WithMessage("Field of study cannot exceed 150 characters");

            RuleFor(x => x.Degree).IsInEnum().WithMessage("Invalid education degree");

            RuleFor(x => x.Start)
                .NotEmpty().WithMessage("Start date is required")
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Start date cannot be in the future");

            RuleFor(x => x.End)
                .GreaterThan(x => x.Start).WithMessage("End date must be after start date")
                .When(x => x.End.HasValue && x.IsGraduated);

            RuleFor(x => x.EducationCertificate)
                .SetValidator(new CertificateDtoValidator());
        }
    }
}
