using ErrorOr;
using Khadamat_SellerPortal.Domain.Common;
using Khadamat_SellerPortal.Domain.Common.Entities;
using Khadamat_SellerPortal.Domain.Common.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable
namespace Khadamat_SellerPortal.Domain.OnlineSellerAggregate
{
    /// <summary>
    /// Represents an online seller profile with personal details, portfolio URLs, social media links,
    /// work experiences, and education history.
    /// </summary>
    public class OnlineSeller : AggregateRoot
    {
        #region Properties And Constructure
        /// <summary>
        /// Gets the seller's personal details including name, contact information, and location.
        /// </summary>
        public SellerPersonalDetails PersonalDetails { get; private set; }

        private readonly List<PortfolioUrl> _portfolioUrls = new();
        private readonly List<SocialMediaLink> _socialMediaLinks = new();
        private readonly List<WorkExperience> _workExperiences = new();
        private readonly List<Education> _educations = new();

        /// <summary>
        /// Gets the read-only collection of the seller's portfolio URLs.
        /// </summary>
        public IReadOnlyCollection<PortfolioUrl> PortfolioUrls => _portfolioUrls.AsReadOnly();

        /// <summary>
        /// Gets the read-only collection of the seller's social media links.
        /// </summary>
        public IReadOnlyCollection<SocialMediaLink> SocialMediaLinks => _socialMediaLinks.AsReadOnly();

        /// <summary>
        /// Gets the read-only collection of the seller's work experiences.
        /// </summary>
        public IReadOnlyCollection<WorkExperience> WorkExperiences => _workExperiences.AsReadOnly();

        /// <summary>
        /// Gets the read-only collection of the seller's education records.
        /// </summary>
        public IReadOnlyCollection<Education> Educations => _educations.AsReadOnly();

        /// <summary>
        /// Initializes a new instance of the <see cref="OnlineSeller"/> class.
        /// </summary>
        /// <param name="firstName">The seller's first name.</param>
        /// <param name="secondName">The seller's second name.</param>
        /// <param name="lastName">The seller's last name.</param>
        /// <param name="email">The seller's email address.</param>
        /// <param name="nationalNo">The seller's national identification number.</param>
        /// <param name="dateOfBirth">The seller's date of birth.</param>
        /// <param name="country">The seller's country of residence.</param>
        /// <param name="city">The seller's city of residence.</param>
        /// <param name="region">The seller's region/state of residence.</param>
        /// <param name="portfolioUrls">Initial collection of portfolio URLs.</param>
        /// <param name="socialMediaLinks">Initial collection of social media links.</param>
        /// <param name="workExperiences">Initial collection of work experiences.</param>
        /// <param name="id">The unique identifier for the seller (defaults to 0 for new entities).</param>
        public OnlineSeller(
            string firstName,
            string secondName,
            string lastName,
            string email,
            string nationalNo,
            DateTime dateOfBirth,
            string country,
            string city,
            string region,
            List<PortfolioUrl> portfolioUrls = default,
            List<SocialMediaLink> socialMediaLinks = default,
            List<WorkExperience> workExperiences = default,
            List<Education> educations = default,
            int id = 0) : base(id)
        {
            PersonalDetails = new SellerPersonalDetails(firstName, secondName, lastName, email, nationalNo, dateOfBirth, country, city, region);
            _portfolioUrls = portfolioUrls ?? new List<PortfolioUrl>();
            _socialMediaLinks = socialMediaLinks ?? new List<SocialMediaLink>();
            _workExperiences = workExperiences ?? new List<WorkExperience>();
            _educations = educations ?? new List<Education>();
        }
        #endregion

        #region Personal Info
        public void UpdatePersonalInfo(
            string firstName,
            string secondName,
            string lastName,
            string email,
            string? nationalNo,
            DateTime dateOfBirth,
            string country,
            string city,
            string region)
        {
            PersonalDetails = new SellerPersonalDetails(firstName, secondName, lastName, email, nationalNo, dateOfBirth, country, city, region);
        }
        #endregion

        #region Portfolio URLs

        /// <summary>
        /// Adds a new portfolio URL for the seller.
        /// </summary>
        /// <param name="url">The URL of the portfolio.</param>
        /// <param name="type">The type of portfolio.</param>
        /// <returns>
        /// <see cref="Success"/> if the operation was successful,
        /// or an error if the portfolio type already exists or URL is invalid.
        /// </returns>
        public ErrorOr<Success> AddPortfolioUrl(string url, PortfolioUrlType type)
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

        /// <summary>
        /// Deletes a portfolio URL by its ID.
        /// </summary>
        /// <param name="id">The ID of the portfolio URL to delete.</param>
        /// <returns>
        /// <see cref="Success"/> if the operation was successful,
        /// or an error if the portfolio doesn't exist or would violate minimum requirements.
        /// </returns>
        public ErrorOr<Success> DeletePortfolioUrl(PortfolioUrlType type)
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

        /// <summary>
        /// Updates the URL for a specific portfolio type.
        /// </summary>
        /// <param name="type">The type of portfolio to update.</param>
        /// <param name="newUrl">The new URL to set.</param>
        /// <returns>
        /// <see cref="Success"/> if the operation was successful,
        /// or an error if the portfolio type doesn't exist or the new URL is invalid.
        /// </returns>
        public ErrorOr<Success> UpdatePortfolioUrlForType(PortfolioUrlType type, string newUrl)
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

        /// <summary>
        /// Adds a new social media link for the seller.
        /// </summary>
        /// <param name="type">The type of social media platform.</param>
        /// <param name="link">The URL of the social media profile.</param>
        /// <returns>
        /// <see cref="Success"/> if the operation was successful,
        /// or an error if the social media type already exists or the link is invalid.
        /// </returns>
        public ErrorOr<Success> AddSocialMediaLink(SocialMediaLinkType type, string link)
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

        /// <summary>
        /// Deletes a social media link by its ID.
        /// </summary>
        /// <param name="id">The ID of the social media link to delete.</param>
        /// <returns>
        /// <see cref="Success"/> if the operation was successful,
        /// or an error if the link doesn't exist.
        /// </returns>
        public ErrorOr<Success> DeleteSocialMediaLink(int id)
        {

            if (!_socialMediaLinks.Any(l => l.Id == id))
                return Error.NotFound("OnlineSeller.SocialLinkNotFound",
                    "No social media link found with this ID");

            var toRemove = _socialMediaLinks.First(l => l.Id == id);
            _socialMediaLinks.Remove(toRemove);
            return Result.Success;
        }

        /// <summary>
        /// Updates the link for a specific social media type.
        /// </summary>
        /// <param name="type">The type of social media to update.</param>
        /// <param name="newLink">The new link to set.</param>
        /// <returns>
        /// <see cref="Success"/> if the operation was successful,
        /// or an error if the social media type doesn't exist or the new link is invalid.
        /// </returns>
        public ErrorOr<Success> UpdateSocialMediaLinkForType(SocialMediaLinkType type, string newLink)
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

        /// <summary>
        /// Adds a new work experience for the seller.
        /// </summary>
        /// <param name="companyName">The name of the company.</param>
        /// <param name="start">The start date of employment.</param>
        /// <param name="end">The end date of employment.</param>
        /// <param name="position">The job position held.</param>
        /// <param name="field">The field of work.</param>
        /// <param name="untilNow">Whether the employment continues to the present.</param>
        /// <returns>
        /// <see cref="Success"/> if the operation was successful,
        /// or an error if the work experience already exists or dates are invalid.
        /// </returns>
        public ErrorOr<Success> AddWorkExperience(string companyName, DateTime start, DateTime? end, string position, string field, bool untilNow = false)
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

        /// <summary>
        /// Deletes a work experience by its ID.
        /// </summary>
        /// <param name="id">The ID of the work experience to delete.</param>
        /// <returns>
        /// <see cref="Success"/> if the operation was successful,
        /// or an error if the work experience doesn't exist.
        /// </returns>
        public ErrorOr<Success> DeleteWorkExperience(int id)
        {
            if (!_workExperiences.Any(w => w.Id == id))
                return Error.NotFound("OnlineSeller.WorkExperienceNotFound",
                    "No work experience found with this ID");

            var toRemove = _workExperiences.First(w => w.Id == id);
            _workExperiences.Remove(toRemove);
            return Result.Success;
        }

        /// <summary>
        /// Updates an existing work experience.
        /// </summary>
        /// <param name="companyName">The name of the company to update.</param>
        /// <param name="position">The new job position.</param>
        /// <param name="field">The new field of work.</param>
        /// <param name="startDate">The new start date.</param>
        /// <param name="endDate">The new end date.</param>
        /// <param name="untilNow">Whether the employment continues to the present.</param>
        /// <returns>
        /// <see cref="Success"/> if the operation was successful,
        /// or an error if the work experience doesn't exist or dates are invalid.
        /// </returns>
        public ErrorOr<Success> UpdateWorkExperience(string companyName, string position, string field, DateTime startDate, DateTime endDate, bool untilNow)
        {
            var existingExperience = _workExperiences.Find(w => w.CompanyName == companyName);
            if (existingExperience == null)
                return Error.NotFound("OnlineSeller.NoExperienceInCompany",
                    $"No {companyName} experience exists to update");


            var updateResult = existingExperience.UpdateWorkExperience(position, field, startDate, endDate, untilNow);
            return updateResult.IsError ? updateResult.FirstError : Result.Success;
        }

        /// <summary>
        /// Calculates the duration of a specific work experience.
        /// </summary>
        /// <param name="companyName">The name of the company to calculate duration for.</param>
        /// <returns>
        /// A tuple containing years, months, and days of experience,
        /// or an error if the work experience doesn't exist.
        /// </returns>
        public ErrorOr<(int Years, int Months, int Days)> CalculateExperienceDuration(string companyName)
        {
            var workExperience = _workExperiences.FirstOrDefault(w => w.CompanyName == companyName);
            if (workExperience == null)
            {
                return Error.NotFound("OnlineSeller.NoExperienceInCompany",
                    $"No {companyName} experience exists to calculate duration");
            }
            return workExperience.CalculateExperienceDuration();
        }

        /// <summary>
        /// Calculates the total duration across all work experiences.
        /// </summary>
        /// <returns>A tuple containing total years, months, and days of experience.</returns>
        public (int Years, int Months, int Days) CalculateExperienceDuration()
        {
            int years = 0;
            int months = 0;
            int days = 0;

            foreach (var workExperience in _workExperiences)
            {
                var result = workExperience.CalculateExperienceDuration();
                years += result.Years;
                months += result.Months;
                days += result.Days;
            }
            return (years, months, days);
        }

        /// <summary>
        /// Formats the duration of a specific work experience as a human-readable string.
        /// </summary>
        /// <param name="companyName">The name of the company to format duration for.</param>
        /// <returns>
        /// A formatted duration string,
        /// or an error if the work experience doesn't exist.
        /// </returns>
        public ErrorOr<string> FormatExperienceDuration(string companyName)
        {
            var workExperience = _workExperiences.FirstOrDefault(w => w.CompanyName == companyName);
            if (workExperience == null)
            {
                return Error.NotFound("OnlineSeller.NoExperienceInCompany",
                    $"No {companyName} experience exists to format duration");
            }
            return workExperience.FormatExperienceDuration();
        }

        /// <summary>
        /// Formats the duration of all work experiences as concatenated strings.
        /// </summary>
        /// <returns>A formatted string containing all work experience durations.</returns>
        public string FormatExperienceDuration()
        {
            string display = "";
            foreach (var workExperience in _workExperiences)
            {
                display += workExperience.FormatExperienceDuration() + "\n";
            }
            return display;
        }

        /// <summary>
        /// Adds a certification to a work experience.
        /// </summary>
        /// <param name="workExperienceId">The ID of the work experience.</param>
        /// <param name="filePath">The path to the certificate file.</param>
        /// <param name="description">The description of the certification.</param>
        /// <returns>
        /// <see cref="Success"/> if the operation was successful,
        /// or an error if the work experience doesn't exist or certification is invalid.
        /// </returns>
        public ErrorOr<Success> AddWorkExperienceCertification(int workExperienceId, string filePath, string description)
        {
            var workEperience = _workExperiences.Find(w => w.Id == workExperienceId);
            if (workEperience == null)
            {
                return Error.NotFound("OnlineSeller.NoWorkExperience", "There is no work experience with the provided id");
            }
            return workEperience.AddCertificate(filePath, description);
        }

        /// <summary>
        /// Adds a certification to a work experience.
        /// </summary>
        /// <param name="companyName">The Company of the work experience.</param>
        /// <param name="filePath">The path to the certificate file.</param>
        /// <param name="description">The description of the certification.</param>
        /// <returns>
        /// <see cref="Success"/> if the operation was successful,
        /// or an error if the work experience doesn't exist or certification is invalid.
        /// </returns>
        public ErrorOr<Success> AddWorkExperienceCertification(string companyName, string filePath, string description)
        {
            var workEperience = _workExperiences.Find(w => w.CompanyName == companyName);
            if (workEperience == null)
            {
                return Error.NotFound("OnlineSeller.NoWorkExperience", "There is no work experience for the provided company");
            }
            return workEperience.AddCertificate(filePath, description);
        }

        /// <summary>
        /// Deletes a certification from a work experience.
        /// </summary>
        /// <param name="workExperienceId">The ID of the work experience.</param>
        /// <param name="certificateId">The ID of the certificate to delete.</param>
        /// <returns>
        /// <see cref="Success"/> if the operation was successful,
        /// or an error if the work experience or certificate doesn't exist.
        /// </returns>
        public ErrorOr<Success> DeleteCertificateFromWorkExperience(int workExperienceId, int certificateId)
        {
            var workEperience = _workExperiences.Find(w => w.Id == workExperienceId);
            if (workEperience == null)
            {
                return Error.NotFound("OnlineSeller.NoWorkExperience", "There is no work experience with the provided id");
            }
            return workEperience.DeleteCertification(certificateId);
        }

        /// <summary>
        /// Updates a certification for a work experience.
        /// </summary>
        /// <param name="workExperienceId">The ID of the work experience.</param>
        /// <param name="certificateId">The ID of the certificate to update.</param>
        /// <param name="filePath">The new path to the certificate file.</param>
        /// <param name="description">The new description of the certification.</param>
        /// <returns>
        /// <see cref="Success"/> if the operation was successful,
        /// or an error if the work experience or certificate doesn't exist.
        /// </returns>
        ErrorOr<Success> UpdateCertificate(int workExperienceId, int certificateId, string filePath, string description)
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

        /// <summary>
        /// Adds a new education record for the seller.
        /// </summary>
        /// <param name="institution">Name of the educational institution.</param>
        /// <param name="fieldOfStudy">Field or major of study.</param>
        /// <param name="degree">Type of degree earned.</param>
        /// <param name="start">Start date of education.</param>
        /// <param name="end">End date of education.</param>
        /// <param name="isGraduated">Whether the education was completed.</param>
        /// <returns>
        /// <see cref="Success"/> if the operation was successful,
        /// or an error if the education record already exists or dates are invalid.
        /// </returns>
        public ErrorOr<Success> AddEducation(string institution, string fieldOfStudy, EducationDegree degree,
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

        /// <summary>
        /// Deletes an education record by ID.
        /// </summary>
        /// <param name="id">ID of the education record to delete.</param>
        /// <returns>
        /// <see cref="Success"/> if the operation was successful,
        /// or an error if the education record doesn't exist.
        /// </returns>
        public ErrorOr<Success> DeleteEducation(int id)
        {
            if (!_educations.Any(e => e.Id == id))
                return Error.NotFound("OnlineSeller.EducationNotFound", "No education record found with this ID");

            var toRemove = _educations.First(e => e.Id == id);
            _educations.Remove(toRemove);
            return Result.Success;
        }

        /// <summary>
        /// Updates an existing education record.
        /// </summary>
        /// <param name="institution">Updated institution name.</param>
        /// <param name="fieldOfStudy">Updated field of study.</param>
        /// <param name="degree">Updated degree type.</param>
        /// <param name="startDate">Updated start date.</param>
        /// <param name="endDate">Updated end date.</param>
        /// <param name="isGraduated">Updated graduation status.</param>
        /// <returns>
        /// <see cref="Success"/> if the operation was successful,
        /// or an error if the education record doesn't exist or dates are invalid.
        /// </returns>
        public ErrorOr<Success> UpdateEducation(string institution, string fieldOfStudy, EducationDegree degree,
            DateTime startDate, DateTime endDate, bool isGraduated)
        {
            var existingEducation = _educations.Find(e => e.Institution == institution && e.FieldOfStudy == fieldOfStudy);
            if (existingEducation == null)
                return Error.NotFound("OnlineSeller.NoEducationRecord",
                    $"No education record exists for {institution} - {fieldOfStudy}");

            var updateResult = existingEducation.UpdateEducation(institution, fieldOfStudy, degree, startDate, endDate, isGraduated);
            return updateResult.IsError ? updateResult.FirstError : Result.Success;
        }

        /// <summary>
        /// Calculates the duration of a specific education record.
        /// </summary>
        /// <param name="institution">Institution name to calculate duration for.</param>
        /// <param name="fieldOfStudy">Field of study to calculate duration for.</param>
        /// <returns>
        /// A tuple containing years, months, and days of education,
        /// or an error if the education record doesn't exist.
        /// </returns>
        public ErrorOr<(int Years, int Months, int Days)> CalculateEducationDuration(string institution, string fieldOfStudy)
        {
            var education = _educations.FirstOrDefault(e => e.Institution == institution && e.FieldOfStudy == fieldOfStudy);
            if (education == null)
            {
                return Error.NotFound("OnlineSeller.NoEducationRecord",
                    $"No education record exists for {institution} - {fieldOfStudy}");
            }
            return education.CalculateEducationDuration();
        }

        /// <summary>
        /// Calculates the total duration across all education records.
        /// </summary>
        /// <returns>A tuple containing total years, months, and days of education.</returns>
        public (int Years, int Months, int Days) CalculateTotalEducationDuration()
        {
            int years = 0;
            int months = 0;
            int days = 0;

            foreach (var education in _educations)
            {
                var result = education.CalculateEducationDuration();
                years += result.Years;
                months += result.Months;
                days += result.Days;
            }
            return (years, months, days);
        }

        /// <summary>
        /// Formats the duration of a specific education record as a human-readable string.
        /// </summary>
        /// <param name="institution">Institution name to format duration for.</param>
        /// <param name="fieldOfStudy">Field of study to format duration for.</param>
        /// <returns>
        /// A formatted duration string,
        /// or an error if the education record doesn't exist.
        /// </returns>
        public ErrorOr<string> FormatEducationDuration(string institution, string fieldOfStudy)
        {
            var education = _educations.FirstOrDefault(e => e.Institution == institution && e.FieldOfStudy == fieldOfStudy);
            if (education == null)
            {
                return Error.NotFound("OnlineSeller.NoEducationRecord",
                    $"No education record exists for {institution} - {fieldOfStudy}");
            }
            return education.FormatEducationDuration();
        }

        /// <summary>
        /// Formats the duration of all education records as concatenated strings.
        /// </summary>
        /// <returns>A formatted string containing all education durations.</returns>
        public string FormatAllEducationDurations()
        {
            var display = new StringBuilder();
            foreach (var education in _educations)
            {
                display.AppendLine(education.FormatEducationDuration());
            }
            return display.ToString();
        }

        /// <summary>
        /// Adds a certificate to an education record.
        /// </summary>
        /// <param name="educationId">ID of the education record.</param>
        /// <param name="filePath">Path to the certificate file.</param>
        /// <param name="description">Description of the certificate.</param>
        /// <returns>
        /// <see cref="Success"/> if the operation was successful,
        /// or an error if the education record doesn't exist or certificate is invalid.
        /// </returns>
        public ErrorOr<Success> AddEducationCertificate(int educationId, string filePath, string description)
        {
            var education = _educations.Find(e => e.Id == educationId);
            if (education == null)
            {
                return Error.NotFound("OnlineSeller.NoEducationRecord", "No education record found with this ID");
            }
            return education.AddCertificate(filePath, description);
        }

        /// <summary>
        /// Adds a certificate to an education record, search for the education based on both fieldOfStudy and institution.
        /// </summary>
        /// <param name="institution">Institution of the education record.</param>
        /// <param name="fieldOfStudy">Field Of Study of the education record.</param>
        /// <param name="filePath">Path to the certificate file.</param>
        /// <param name="description">Description of the certificate.</param>
        /// <returns>
        /// <see cref="Success"/> if the operation was successful,
        /// or an error if the education record doesn't exist or certificate is invalid.
        /// </returns>
        public ErrorOr<Success> AddEducationCertificate(string institution, string fieldOfStudy, string filePath, string description)
        {

            var education = _educations.Find(e => e.Institution == institution && e.FieldOfStudy == fieldOfStudy);
            if (education == null)
            {
                return Error.NotFound("OnlineSeller.NoEducationRecord", "No education record found for the provided institution and field of study");
            }
            return education.AddCertificate(filePath, description);
        }

        /// <summary>
        /// Deletes a certificate from an education record.
        /// </summary>
        /// <param name="educationId">ID of the education record.</param>
        /// <returns>
        /// <see cref="Success"/> if the operation was successful,
        /// or an error if the education record doesn't exist.
        /// </returns>
        public ErrorOr<Success> DeleteEducationCertificate(int educationId)
        {
            var education = _educations.Find(e => e.Id == educationId);
            if (education == null)
            {
                return Error.NotFound("OnlineSeller.NoEducationRecord", "No education record found with this ID");
            }
            return education.DeleteCertificate();
        }

        /// <summary>
        /// Updates a certificate for an education record.
        /// </summary>
        /// <param name="educationId">ID of the education record.</param>
        /// <param name="filePath">New path to the certificate file.</param>
        /// <param name="description">New description of the certificate.</param>
        /// <returns>
        /// <see cref="Success"/> if the operation was successful,
        /// or an error if the education record doesn't exist or certificate is invalid.
        /// </returns>
        public ErrorOr<Success> UpdateEducationCertificate(int educationId, string filePath, string description)
        {
            var education = _educations.Find(e => e.Id == educationId);
            if (education == null)
            {
                return Error.NotFound("OnlineSeller.NoEducationRecord", "No education record found with this ID");
            }
            return education.UpdateCertificate(filePath, description);
        }
        #endregion

        /// <summary>
        /// Private constructor for ORM use only.
        /// </summary>
        private OnlineSeller()
        {

        }
    }
}
