using ErrorOr;
using Khadamat_SellerPortal.Domain.Common.ValueObjects;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Domain.Common.Entities
{
    /// <summary>
    /// Represents a work experience record containing employment details, duration, and associated certifications.
    /// </summary>
    public class WorkExperience : Entity
    {
        /// <summary>
        /// Gets the name of the company where the work experience was gained.
        /// </summary>
        public string CompanyName { get; private set; }

        /// <summary>
        /// Gets the date range of the work experience (start and end dates).
        /// </summary>
        public DateRange Range { get; private set; }

        /// <summary>
        /// Gets the job position held during this work experience.
        /// </summary>
        public string Position { get; private set; }

        /// <summary>
        /// Gets the field or industry of this work experience.
        /// </summary>
        public string Field { get; private set; } // Convert to an enum with predefined fields

        /// <summary>
        /// Gets the ID of the seller associated with this work experience.
        /// </summary>
        public int SellerId { get; private set; }

        private readonly List<Certificate> _certificates = new();

        /// <summary>
        /// Gets a read-only collection of certificates associated with this work experience.
        /// </summary>
        public IReadOnlyCollection<Certificate> Certificates => _certificates.AsReadOnly();

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkExperience"/> class.
        /// </summary>
        /// <param name="comapnyName">Name of the company.</param>
        /// <param name="range">Date range of the employment.</param>
        /// <param name="position">Job position held.</param>
        /// <param name="field">Field or industry of work.</param>
        /// <param name="sellerId">ID of the associated seller.</param>
        /// <param name="id">Unique identifier for the work experience (defaults to 0 for new entities).</param>
        private WorkExperience(string comapnyName, DateRange range, string position, string field, int sellerId, int id = 0) : base(id)
        {
            CompanyName = comapnyName;
            Range = range;
            Position = position;
            Field = field;
            SellerId = sellerId;
        }
        /// <summary>
        /// Creates a new work experience record.
        /// </summary>
        /// <param name="comapnyName">Name of the company.</param>
        /// <param name="start">Start date of employment.</param>
        /// <param name="end">End date of employment.</param>
        /// <param name="position">Job position held.</param>
        /// <param name="field">Field or industry of work.</param>
        /// <param name="sellerId">ID of the associated seller.</param>
        /// <param name="untilNow">Indicates if the employment continues to the present.</param>
        /// <returns>
        /// A new <see cref="WorkExperience"/> instance or an error if date validation fails.
        /// </returns>
        public static ErrorOr<WorkExperience> Create(string comapnyName, DateTime start, DateTime? end, string position, string field, int sellerId, bool untilNow = false)
        {
            var createRangeRes = DateRange.FromDateTimes(start, end, untilNow);
            if (createRangeRes.IsError)
            {
                return createRangeRes.FirstError;
            }
            return new WorkExperience(comapnyName, createRangeRes.Value, position, field, sellerId);
        }

        /// <summary>
        /// Updates the work experience details.
        /// </summary>
        /// <param name="position">New job position.</param>
        /// <param name="field">New field or industry.</param>
        /// <param name="startDate">New start date.</param>
        /// <param name="endDate">New end date.</param>
        /// <param name="untilNow">Indicates if the employment continues to the present.</param>
        /// <returns>
        /// <see cref="Success"/> if the update was successful,
        /// or an error if date validation fails.
        /// </returns>
        public ErrorOr<Success> UpdateWorkExperience(string position, string field, DateTime startDate, DateTime endDate, bool untilNow)
        {
            var newDateRangeRes = DateRange.FromDateTimes(startDate, endDate, untilNow);
            if (newDateRangeRes.IsError)
            {
                return newDateRangeRes.FirstError;
            }
            Range = newDateRangeRes.Value;
            Position = position;
            Field = field;

            return Result.Success;
        }

        /// <summary>
        /// Calculates the duration of this work experience.
        /// </summary>
        /// <returns>
        /// A tuple containing the years, months, and days of experience.
        /// </returns>
        public (int Years, int Months, int Days) CalculateExperienceDuration()
        {
            return Range.CalculateDuration();
        }

        /// <summary>
        /// Formats the work experience duration as a human-readable string.
        /// </summary>
        /// <returns>A formatted duration string.</returns>
        public string FormatExperienceDuration()
        {
            return Range.FormatDuration();
        }

        /// <summary>
        /// Adds a certificate to this work experience.
        /// </summary>
        /// <param name="filePath">Path to the certificate file.</param>
        /// <param name="description">Description of the certificate.</param>
        /// <returns>
        /// <see cref="Success"/> if the certificate was added successfully,
        /// or an error if the file path already exists.
        /// </returns>
        public ErrorOr<Success> AddCertificate(string filePath, string description)
        {
            if (_certificates.Any(c => c.FilePath == filePath))
            {
                return Error.Conflict("WorkExperience.CertificateExist", "The provided path already have a certificate");
            }
            var certificate = new Certificate(filePath, description, workExperienceId: Id, educationId: null);
            _certificates.Add(certificate);
            return Result.Success;
        }

        /// <summary>
        /// Deletes a certification from this work experience.
        /// </summary>
        /// <param name="certificationId">ID of the certificate to delete.</param>
        /// <returns>
        /// <see cref="Success"/> if the certificate was deleted successfully,
        /// or an error if no certificate with the specified ID was found.
        /// </returns>
        public ErrorOr<Success> DeleteCertification(int certificationId)
        {
            var certification = _certificates.Find(c => c.Id == certificationId);
            if (certification == null)
            {
                return Error.NotFound("WorkExperience.NoCertificateFound", "No certificate with such id found");
            }
            _certificates.Remove(certification);
            return Result.Success;
        }

        /// <summary>
        /// Updates an existing certificate for this work experience.
        /// </summary>
        /// <param name="certificationId">ID of the certificate to update.</param>
        /// <param name="filePath">New path to the certificate file.</param>
        /// <param name="description">New description of the certificate.</param>
        /// <returns>
        /// <see cref="Success"/> if the certificate was updated successfully,
        /// or an error if no certificate with the specified ID was found.
        /// </returns>
        public ErrorOr<Success> UpdateCertificate(int certificationId, string filePath, string description)
        {
            var certification = _certificates.Find(c => c.Id == certificationId);
            if (certification == null)
            {
                return Error.NotFound("WorkExperience.NoCertificateFound", "No certificate with such id found");
            }
            return certification.UpdateCertificate(filePath, description);
        }
        private WorkExperience()
        {
            
        }
    }
}
