using FluentValidation;
using Khadamat_SellerPortal.Application.OnlineSellers.Commands;
using Khadamat_SellerPortal.Application.OnlineSellers.Common;

namespace Khadamat_SellerPortal.Application.Common.EntitiesValidators
{
    public class WorkExperienceDtoValidator : AbstractValidator<WorkExperienceDto>
    {
        public WorkExperienceDtoValidator()
        {
            RuleFor(x => x.CompanyName)
                .NotEmpty().WithMessage("Company name is required")
                .MaximumLength(200).WithMessage("Company name cannot exceed 200 characters");

            RuleFor(x => x.Start)
                .NotEmpty().WithMessage("Start date is required")
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Start date cannot be in the future");

            RuleFor(x => x.End)
                .GreaterThan(x => x.Start).WithMessage("End date must be after start date")
                .When(x => x.End.HasValue && !x.UntilNow);

            RuleFor(x => x.Position)
                .NotEmpty().WithMessage("Position is required")
                .MaximumLength(100).WithMessage("Position cannot exceed 50 characters");

            RuleFor(x => x.Field)
                .NotEmpty().WithMessage("Field is required")
                .MaximumLength(50).WithMessage("Field cannot exceed 50 characters");

            RuleFor(x => x.Certificates)
                //.Must(c => c == null || c.Count <= 5).WithMessage("Cannot have more than 5 certificates per work experience")
                .ForEach(c => c.SetValidator(new CertificateDtoValidator()));
        }
    }
}
