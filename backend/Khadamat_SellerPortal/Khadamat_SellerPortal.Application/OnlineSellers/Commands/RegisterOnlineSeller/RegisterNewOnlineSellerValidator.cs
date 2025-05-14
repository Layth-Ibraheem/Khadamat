using FluentValidation;
using Khadamat_SellerPortal.Application.Common.EntitiesValidators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Application.OnlineSellers.Commands.RegisterOnlineSeller
{
    public class RegisterNewOnlineSellerCommandValidator : AbstractValidator<RegisterNewOnlineSellerCommand>
    {
        public RegisterNewOnlineSellerCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required")
                .MaximumLength(20).WithMessage("First name cannot exceed 20 characters");
            //.Matches(@"^[a-zA-Z\s\-']+$").WithMessage("First name contains invalid characters");

            RuleFor(x => x.SecondName)
                .MaximumLength(20).WithMessage("Second name cannot exceed 20 characters");
            //.Matches(@"^[a-zA-Z\s\-']*$").WithMessage("Second name contains invalid characters")
            //.When(x => !string.IsNullOrEmpty(x.SecondName));

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required")
                .MaximumLength(20).WithMessage("Last name cannot exceed 20 characters");
            //.Matches(@"^[a-zA-Z\s\-']+$").WithMessage("Last name contains invalid characters");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("A valid email is required")
                .MaximumLength(250).WithMessage("Email cannot exceed 250 characters");

            RuleFor(x => x.NationalNo)
                .MaximumLength(50).WithMessage("National number cannot exceed 50 characters")
                .When(x => !string.IsNullOrEmpty(x.NationalNo));

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Date of birth is required")
                .LessThan(DateTime.Today.AddYears(-18)).WithMessage("Seller must be at least 18 years old")
                .GreaterThan(DateTime.Today.AddYears(-100)).WithMessage("Invalid date of birth");

            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Country is required")
                .MaximumLength(50).WithMessage("Country name cannot exceed 50 characters");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City is required")
                .MaximumLength(50).WithMessage("City name cannot exceed 50 characters");

            RuleFor(x => x.Region)
                .MaximumLength(100).WithMessage("Region name cannot exceed 100 characters");
            //.When(x => !string.IsNullOrEmpty(x.Region));

            RuleFor(x => x.WorkExperiences)
                //.Must(we => we == null || we.Count <= 10).WithMessage("Cannot have more than 10 work experiences")
                .ForEach(we => we.SetValidator(new WorkExperienceDtoValidator()));

            RuleFor(x => x.Educations)
                //.Must(e => e == null || e.Count <= 5).WithMessage("Cannot have more than 5 education entries")
                .ForEach(e => e.SetValidator(new EducationDtoValidator()));

            RuleFor(x => x.PortfolioUrls)
                //.Must(p => p == null || p.Count <= 5).WithMessage("Cannot have more than 5 portfolio URLs")
                .ForEach(p => p.SetValidator(new PortfolioUrlDtoValidator()));

            RuleFor(x => x.SocialMediaLinks)
                //.Must(s => s == null || s.Count <= 5).WithMessage("Cannot have more than 5 social media links")
                .ForEach(s => s.SetValidator(new SocialMediaLinkDtoValidator()));
        }
    }
}
