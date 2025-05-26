using ErrorOr;
using Khadamat_SellerPortal.Domain.Common;
using Khadamat_SellerPortal.Domain.Common.Entities;
using Khadamat_SellerPortal.Domain.Common.ValueObjects;
using Khadamat_SellerPortal.Domain.SellerAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable
namespace Khadamat_SellerPortal.Domain.OnlineSellerAggregate
{
    /// <summary>
    /// Represents an online seller
    /// </summary>
    public class OnlineSeller : Seller
    {
        public OnlineSeller(string firstName, string secondName, string lastName, string email, string nationalNo, DateTime dateOfBirth, string country, string city, string region, int id = 0) : base(firstName, secondName, lastName, email, nationalNo, dateOfBirth, country, city, region, id)
        {
        }

        #region Personal Info
        public override void UpdatePersonalInfo(
            string firstName,
            string secondName,
            string lastName,
            string email,
            string nationalNo,
            DateTime dateOfBirth,
            string country,
            string city,
            string region)
        {
            PersonalDetails = new SellerPersonalDetails(firstName, secondName, lastName, email, nationalNo, dateOfBirth, country, city, region);
        }
        #endregion

        #region Portfolio URLs

        public override ErrorOr<Success> AddPortfolioUrl(string url, PortfolioUrlType type)
        {

            if (_portfolioUrls.Any(p => p.Type == type))
            {
                return Error.Conflict("OnlineSeller.PortfolioExists", $"Already have {type.Name} Portfolio Url");
            }
            var createPortfolioUrlRes = PortfolioUrl.Create(type, url, Id);
            if (createPortfolioUrlRes.IsError)
            {
                return createPortfolioUrlRes.FirstError;
            }
            _portfolioUrls.Add(createPortfolioUrlRes.Value);

            return Result.Success;
        }

        public override ErrorOr<Success> DeletePortfolioUrl(PortfolioUrlType type)
        {
            // Example of a business rule that must be in the aggregate level
            //if (_portfolioUrls.Count <= 1)
            //{
            //    return Error.Validation("OnlineSeller.MinPortfolios", "You only have one portfolio, at least one portfolio is required");
            //}
            if (!_portfolioUrls.Any(p => p.Type == type))
            {
                return Error.Conflict("OnlineSeller.PortfolioDon`tExist", "This seller doesn`t have such portfolio");
            }
            _portfolioUrls.Remove(_portfolioUrls.First(p => p.Type == type));
            return Result.Success;
        }

        public override ErrorOr<Success> UpdatePortfolioUrlForType(PortfolioUrlType type, string newUrl)
        {
            var currentPortfolio = _portfolioUrls.Find(p => p.Type.Equals(type));

            if (currentPortfolio == null)
            {
                return Error.NotFound("OnlineSeller.Don`tHavePortfolioType", "This seller doesn`t have such a url type");
            }
            if (currentPortfolio.Url != newUrl)
            {
                var result = currentPortfolio.UpdateUrl(newUrl);
                if (result.IsError)
                    return result.FirstError;
            }
            return Result.Success;
        }

        #endregion

        #region Social Media Links

        public override ErrorOr<Success> AddSocialMediaLink(SocialMediaLinkType type, string link)
        {

            // Prevent duplicates
            if (_socialMediaLinks.Any(l => l.Type == type))
                return Error.Conflict("OnlineSeller.SocialLinkExists",
                    $"Already have a {type} link");

            // Create new instance to ensure proper ownership
            var createResult = SocialMediaLink.Create(link, type, Id);

            if (createResult.IsError)
            {
                return createResult.FirstError;
            }

            _socialMediaLinks.Add(createResult.Value);
            return Result.Success;
        }

        public override ErrorOr<Success> DeleteSocialMediaLink(int id)
        {

            if (!_socialMediaLinks.Any(l => l.Id == id))
                return Error.NotFound("OnlineSeller.SocialLinkNotFound",
                    "No social media link found with this ID");

            var toRemove = _socialMediaLinks.First(l => l.Id == id);
            _socialMediaLinks.Remove(toRemove);
            return Result.Success;
        }

        public override ErrorOr<Success> UpdateSocialMediaLinkForType(SocialMediaLinkType type, string newLink)
        {
            var existingLink = _socialMediaLinks.Find(l => l.Type == type);
            if (existingLink == null)
                return Error.NotFound("OnlineSeller.NoLinkOfType",
                    $"No {type} link exists to update");

            if (existingLink.Link == newLink)
                return Result.Success;

            var updateResult = existingLink.UpdateLink(newLink);
            return updateResult.IsError ? updateResult.FirstError : Result.Success;
        }

        #endregion

        #region Work Experiences

        public override ErrorOr<Success> AddWorkExperience(string companyName, DateTime start, DateTime? end, string position, string field, bool untilNow = false)
        {

            if (_workExperiences.Any(w => w.CompanyName == companyName))
            {
                return Error.Conflict("OnlineSeller.ExperienceExistForComapany", "This experience already exists for the provided company");
            }
            var createWorkExperienceRes = WorkExperience.Create(companyName, start, end, position, field, Id, untilNow);
            if (createWorkExperienceRes.IsError)
            {
                return createWorkExperienceRes.FirstError;
            }
            _workExperiences.Add(createWorkExperienceRes.Value);

            return Result.Success;
        }

        public override ErrorOr<Success> DeleteWorkExperience(int id)
        {
            if (!_workExperiences.Any(w => w.Id == id))
                return Error.NotFound("OnlineSeller.WorkExperienceNotFound",
                    "No work experience found with this ID");

            var toRemove = _workExperiences.First(w => w.Id == id);
            _workExperiences.Remove(toRemove);
            return Result.Success;
        }

        public override ErrorOr<Success> UpdateWorkExperience(string companyName, string position, string field, DateTime startDate, DateTime endDate, bool untilNow)
        {
            var existingExperience = _workExperiences.Find(w => w.CompanyName == companyName);
            if (existingExperience == null)
                return Error.NotFound("OnlineSeller.NoExperienceInCompany",
                    $"No {companyName} experience exists to update");


            var updateResult = existingExperience.UpdateWorkExperience(position, field, startDate, endDate, untilNow);
            return updateResult.IsError ? updateResult.FirstError : Result.Success;
        }

        public override ErrorOr<Success> AddWorkExperienceCertification(int workExperienceId, string filePath, string description)
        {
            var workEperience = _workExperiences.Find(w => w.Id == workExperienceId);
            if (workEperience == null)
            {
                return Error.NotFound("OnlineSeller.NoWorkExperience", "There is no work experience with the provided id");
            }
            return workEperience.AddCertificate(filePath, description);
        }

        public override ErrorOr<Success> AddWorkExperienceCertification(string companyName, string filePath, string description)
        {
            var workEperience = _workExperiences.Find(w => w.CompanyName == companyName);
            if (workEperience == null)
            {
                return Error.NotFound("OnlineSeller.NoWorkExperience", "There is no work experience for the provided company");
            }
            return workEperience.AddCertificate(filePath, description);
        }

        public override ErrorOr<Success> DeleteCertificateFromWorkExperience(int workExperienceId, int certificateId)
        {
            var workEperience = _workExperiences.Find(w => w.Id == workExperienceId);
            if (workEperience == null)
            {
                return Error.NotFound("OnlineSeller.NoWorkExperience", "There is no work experience with the provided id");
            }
            return workEperience.DeleteCertification(certificateId);
        }

        public override ErrorOr<Success> UpdateCertificate(int workExperienceId, int certificateId, string filePath, string description)
        {
            var workEperience = _workExperiences.Find(w => w.Id == workExperienceId);
            if (workEperience == null)
            {
                return Error.NotFound("OnlineSeller.NoWorkExperience", "There is no work experience with the provided id");
            }
            return workEperience.UpdateCertificate(certificateId, filePath, description);
        }

        #endregion

        #region Education
        public override ErrorOr<Success> AddEducation(string institution, string fieldOfStudy, EducationDegree degree,
            DateTime start, DateTime? end, bool isGraduated = false)
        {
            if (_educations.Any(e => e.Institution == institution && e.FieldOfStudy == fieldOfStudy))
            {
                return Error.Conflict("OnlineSeller.EducationExists",
                    "Education record already exists for this institution and field of study");
            }

            var createResult = Education.Create(institution, fieldOfStudy, degree, start, end, Id, isGraduated);
            if (createResult.IsError)
            {
                return createResult.FirstError;
            }

            _educations.Add(createResult.Value);
            return Result.Success;
        }

        public override ErrorOr<Success> DeleteEducation(int id)
        {
            if (!_educations.Any(e => e.Id == id))
                return Error.NotFound("OnlineSeller.EducationNotFound", "No education record found with this ID");

            var toRemove = _educations.First(e => e.Id == id);
            _educations.Remove(toRemove);
            return Result.Success;
        }

        public override ErrorOr<Success> UpdateEducation(string institution, string fieldOfStudy, EducationDegree degree,
            DateTime startDate, DateTime endDate, bool isGraduated)
        {
            var existingEducation = _educations.Find(e => e.Institution == institution && e.FieldOfStudy == fieldOfStudy);
            if (existingEducation == null)
                return Error.NotFound("OnlineSeller.NoEducationRecord",
                    $"No education record exists for {institution} - {fieldOfStudy}");

            var updateResult = existingEducation.UpdateEducation(institution, fieldOfStudy, degree, startDate, endDate, isGraduated);
            return updateResult.IsError ? updateResult.FirstError : Result.Success;
        }

        public override ErrorOr<Success> AddEducationCertificate(int educationId, string filePath, string description)
        {
            var education = _educations.Find(e => e.Id == educationId);
            if (education == null)
            {
                return Error.NotFound("OnlineSeller.NoEducationRecord", "No education record found with this ID");
            }
            return education.AddCertificate(filePath, description);
        }

        public override ErrorOr<Success> AddEducationCertificate(string institution, string fieldOfStudy, string filePath, string description)
        {

            var education = _educations.Find(e => e.Institution == institution && e.FieldOfStudy == fieldOfStudy);
            if (education == null)
            {
                return Error.NotFound("OnlineSeller.NoEducationRecord", "No education record found for the provided institution and field of study");
            }
            return education.AddCertificate(filePath, description);
        }

        public override ErrorOr<Success> DeleteEducationCertificate(int educationId)
        {
            var education = _educations.Find(e => e.Id == educationId);
            if (education == null)
            {
                return Error.NotFound("OnlineSeller.NoEducationRecord", "No education record found with this ID");
            }
            return education.DeleteCertificate();
        }

        public override ErrorOr<Success> UpdateEducationCertificate(int educationId, string filePath, string description)
        {
            var education = _educations.Find(e => e.Id == educationId);
            if (education == null)
            {
                return Error.NotFound("OnlineSeller.NoEducationRecord", "No education record found with this ID");
            }
            return education.UpdateCertificate(filePath, description);
        }

        public override ErrorOr<Success> DeleteSocialMediaLink(SocialMediaLinkType type)
        {
            if (!_socialMediaLinks.Any(l => l.Type == type))
                return Error.NotFound("OnlineSeller.SocialLinkNotFound",
                    "No social media link found with this ID");

            var toRemove = _socialMediaLinks.First(l => l.Type == type);
            _socialMediaLinks.Remove(toRemove);
            return Result.Success;
        }
        #endregion

        private OnlineSeller()
        {
            
        }

    }
}
