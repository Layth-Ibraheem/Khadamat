using ErrorOr;
using Khadamat_SellerPortal.Application.Common.Interfcaes;
using Khadamat_SellerPortal.Application.OnlineSellers.Common;
using Khadamat_SellerPortal.Domain.OnlineSellerAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Application.OnlineSellers.Commands.RegisterOnlineSeller
{
    public class RegisterNewOnlineSellerCommandHandler : IRequestHandler<RegisterNewOnlineSellerCommand, ErrorOr<OnlineSeller>>
    {
        private readonly IOnlineSellerRepository _onlineSellerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterNewOnlineSellerCommandHandler(IOnlineSellerRepository onlineSellerRepository, IUnitOfWork unitOfWork)
        {
            _onlineSellerRepository = onlineSellerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<OnlineSeller>> Handle(RegisterNewOnlineSellerCommand request, CancellationToken cancellationToken)
        {
            //var onlineSeller = CreateOnlineSeller(request);

            //var educationResult = ProcessEducations(request, onlineSeller);
            //if (educationResult.IsError) return educationResult.FirstError;

            //var workExperienceResult = ProcessWorkExperiences(request, onlineSeller);
            //if (workExperienceResult.IsError) return workExperienceResult.FirstError;

            //var portfolioResult = ProcessPortfolios(request, onlineSeller);
            //if (portfolioResult.IsError) return portfolioResult.FirstError;

            //var socialMediaResult = ProcessSocialMediaLinks(request, onlineSeller);
            //if (socialMediaResult.IsError) return socialMediaResult.FirstError;

            var onlineSeller = new OnlineSeller(request.FirstName, request.SecondName, request.LastName, request.Email, request.NationalNo, request.DateOfBirth,
                request.Country, request.City, request.Region);

            await _onlineSellerRepository.AddOnlineSeller(onlineSeller);
            await _unitOfWork.CommitChangesAsync();
            return onlineSeller;
        }

        //private OnlineSeller CreateOnlineSeller(RegisterNewOnlineSellerCommand request)
        //{
        //    return new OnlineSeller(
        //        request.FirstName,
        //        request.SecondName,
        //        request.LastName,
        //        request.Email,
        //        request.NationalNo,
        //        request.DateOfBirth,
        //        request.Country,
        //        request.City,
        //        request.Region);
        //}

        //private ErrorOr<bool> ProcessEducations(RegisterNewOnlineSellerCommand request, OnlineSeller onlineSeller)
        //{
        //    foreach (var education in request.Educations)
        //    {
        //        var addEducationRes = onlineSeller.AddEducation(
        //            education.Institution,
        //            education.FieldOfStudy,
        //            education.Degree,
        //            education.Start,
        //            education.End,
        //            education.IsGraduated);

        //        if (addEducationRes.IsError) return addEducationRes.FirstError;

        //        if (education.EducationCertificate is not null)
        //        {
        //            var certificateResult = ProcessEducationCertificate(onlineSeller, education);
        //            if (certificateResult.IsError) return certificateResult.FirstError;
        //        }
        //    }
        //    return true;
        //}

        //private ErrorOr<bool> ProcessEducationCertificate(OnlineSeller onlineSeller, EducationDto education)
        //{
        //    var addCertResult = onlineSeller.AddEducationCertificate(
        //        education.Institution,
        //        education.FieldOfStudy,
        //        education.EducationCertificate.FilePath,
        //        education.EducationCertificate.Description);

        //    return addCertResult.IsError ? addCertResult.FirstError : true;
        //}

        //private ErrorOr<bool> ProcessWorkExperiences(RegisterNewOnlineSellerCommand request, OnlineSeller onlineSeller)
        //{
        //    foreach (var work in request.WorkExperiences)
        //    {
        //        var addWorkRes = onlineSeller.AddWorkExperience(
        //            work.CompanyName,
        //            work.Start,
        //            work.End,
        //            work.Position,
        //            work.Field,
        //            work.UntilNow);

        //        if (addWorkRes.IsError) return addWorkRes.FirstError;

        //        foreach (var certificate in work.Certificates)
        //        {
        //            var certificateResult = ProcessWorkCertificate(onlineSeller, work, certificate);
        //            if (certificateResult.IsError) return certificateResult.FirstError;
        //        }
        //    }
        //    return true;
        //}

        //private ErrorOr<bool> ProcessWorkCertificate(OnlineSeller onlineSeller, WorkExperienceDto work, CertificateDto certificate)
        //{

        //    var addCertResult = onlineSeller.AddWorkExperienceCertification(
        //        work.CompanyName,
        //        certificate.FilePath,
        //        certificate.Description);

        //    return addCertResult.IsError ? addCertResult.FirstError : true;
        //}

        //private ErrorOr<bool> ProcessPortfolios(RegisterNewOnlineSellerCommand request, OnlineSeller onlineSeller)
        //{
        //    foreach (var portfolio in request.PortfolioUrls)
        //    {
        //        var addResult = onlineSeller.AddPortfolioUrl(portfolio.Url, portfolio.Type);
        //        if (addResult.IsError) return addResult.FirstError;
        //    }
        //    return true;
        //}

        //private ErrorOr<bool> ProcessSocialMediaLinks(RegisterNewOnlineSellerCommand request, OnlineSeller onlineSeller)
        //{
        //    foreach (var link in request.SocialMediaLinks)
        //    {
        //        var addResult = onlineSeller.AddSocialMediaLink(link.Type, link.Link);
        //        if (addResult.IsError) return addResult.FirstError;
        //    }
        //    return true;
        //}
    }
}
