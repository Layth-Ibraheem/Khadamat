using ErrorOr;
using Khadamat_SellerPortal.Domain.Common.Entities.EducationEntity;
using Khadamat_SellerPortal.Domain.Common.Entities.PortfolioUrlEntity;
using Khadamat_SellerPortal.Domain.Common.Entities.SocialMediaLinkEntity;
using Khadamat_SellerPortal.Domain.Common.Interfaces;
using Khadamat_SellerPortal.Domain.SellerAggregate;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Domain.OfflineSellerAggregate
{
    /// <summary>
    /// Represents an offline seller
    /// </summary>
    public class OfflineSeller : Seller
    {
        public OfflineSeller(
            string firstName,
            string secondName,
            string lastName,
            string email,
            string nationalNo,
            DateTime dateOfBirth,
            string country,
            string city,
            string region,
            int id = 0) : base(firstName, secondName, lastName, email, nationalNo, dateOfBirth, country, city, region, id)
        {
        }

        private OfflineSeller()
        {
            
        }

        public override ErrorOr<Success> AddEducation(string institution, string fieldOfStudy, EducationDegree degree, DateTime start, DateTime? end, bool isGraduated = false)
        {
            throw new NotImplementedException();
        }

        public override ErrorOr<Success> AddEducationCertificate(int educationId, string filePath, string description)
        {
            throw new NotImplementedException();
        }

        public override ErrorOr<Success> AddEducationCertificate(string institution, string fieldOfStudy, string filePath, string description)
        {
            throw new NotImplementedException();
        }

        public override ErrorOr<Success> AddPortfolioUrl(string url, PortfolioUrlType type)
        {
            throw new NotImplementedException();
        }

        public override ErrorOr<Success> AddSocialMediaLink(SocialMediaLinkType type, string link)
        {
            throw new NotImplementedException();
        }

        public override ErrorOr<Success> AddWorkExperience(string companyName, DateTime start, DateTime? end, string position, string field, bool untilNow = false)
        {
            throw new NotImplementedException();
        }

        public override ErrorOr<Success> AddWorkExperienceCertification(int workExperienceId, string filePath, string description)
        {
            throw new NotImplementedException();
        }

        public override ErrorOr<Success> AddWorkExperienceCertification(string companyName, string filePath, string description)
        {
            throw new NotImplementedException();
        }

        public override ErrorOr<Success> DeleteCertificateFromWorkExperience(int workExperienceId, int certificateId)
        {
            throw new NotImplementedException();
        }

        public override ErrorOr<Success> DeleteEducation(int id)
        {
            throw new NotImplementedException();
        }

        public override ErrorOr<Success> DeleteEducationCertificate(int educationId)
        {
            throw new NotImplementedException();
        }

        public override ErrorOr<Success> DeletePortfolioUrl(PortfolioUrlType type)
        {
            throw new NotImplementedException();
        }

        public override ErrorOr<Success> DeleteSocialMediaLink(SocialMediaLinkType type)
        {
            throw new NotImplementedException();
        }

        public override ErrorOr<Success> DeleteSocialMediaLink(int id)
        {
            throw new NotImplementedException();
        }

        public override ErrorOr<Success> DeleteWorkExperience(int id)
        {
            throw new NotImplementedException();
        }

        public override ErrorOr<Success> UpdateEducation(string institution, string fieldOfStudy, EducationDegree degree, DateTime startDate, DateTime? endDate, bool isGraduated)
        {
            throw new NotImplementedException();
        }

        public override ErrorOr<Success> UpdateEducationCertificate(int educationId, string filePath, int fileId, out string previousPath, string description = "")
        {
            throw new NotImplementedException();
        }

        public override ErrorOr<Success> UpdateEducationCertificateByInstitutionAndFieldOfStudy(string institution, string fieldOfStudy, string filePath, int fileId, out string previousPath)
        {
            throw new NotImplementedException();
        }

        public override ErrorOr<Success> UpdatePortfolioUrlForType(PortfolioUrlType type, string newUrl)
        {
            throw new NotImplementedException();
        }

        public override ErrorOr<Success> UpdateSocialMediaLinkForType(SocialMediaLinkType type, string newLink)
        {
            throw new NotImplementedException();
        }

        public override ErrorOr<Success> UpdateWorkExperience(string companyName, string position, string field, DateTime startDate, DateTime? endDate, bool untilNow)
        {
            throw new NotImplementedException();
        }

        public override ErrorOr<Success> UpdateWorkExperience(int workExperienceId, string companyName, string position, string field, DateTime startDate, DateTime? endDate, bool untilNow)
        {
            throw new NotImplementedException();
        }

        public override ErrorOr<Success> UpdateWorkExperienceCertificate(int workExperienceId, int certificateId, string filePath, int fileId, out string previousPath, string description = "")
        {
            throw new NotImplementedException();
        }

        public override ErrorOr<Success> UpdateWorkExperienceCertificateByCompanyNameAndPosition(string companyName, string position, int certificateId, string filePath, string description)
        {
            throw new NotImplementedException();
        }
    }
}
