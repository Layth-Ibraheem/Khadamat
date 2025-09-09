using ErrorOr;
using Khadamat_SellerPortal.Domain.Common.Entities.CertificateEntity;
using Khadamat_SellerPortal.Domain.Common.ValueObjects;
#nullable disable
namespace Khadamat_SellerPortal.Domain.Common.Entities.EducationEntity
{
    /// <summary>
    /// Represents a seller's educational background with institution details, field of study, 
    /// degree information, and optional certification
    /// </summary>
    public class Education : Entity
    {
        /// <summary>
        /// Gets the name of the educational institution.
        /// </summary>
        public string Institution { get; private set; }

        /// <summary>
        /// Gets the field or major of study.
        /// </summary>
        public string FieldOfStudy { get; private set; }

        /// <summary>
        /// Gets the degree obtained or being pursued.
        /// </summary>
        public EducationDegree Degree { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the education was successfully completed.
        /// </summary>
        public bool IsGraduated { get; private set; }

        /// <summary>
        /// Gets the date range of attendance (start and end dates).
        /// </summary>
        public DateRange AttendancePeriod { get; private set; }

        /// <summary>
        /// Gets the ID of the seller associated with this education record.
        /// </summary>
        public int SellerId { get; private set; }

        /// <summary>
        /// Gets the certificate associated with this education, if available.
        /// </summary>
        public Certificate EducationCertificate { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Education"/> class.
        /// </summary>
        /// <param name="institution">Name of the educational institution.</param>
        /// <param name="fieldOfStudy">Field or major of study.</param>
        /// <param name="degree">Degree obtained or being pursued.</param>
        /// <param name="attendancePeriod">Date range of attendance.</param>
        /// <param name="sellerId">ID of the associated seller.</param>
        /// <param name="isGraduated">Whether the education was completed (defaults to false).</param>
        /// <param name="id">Unique identifier for the education record (defaults to 0 for new entities).</param>
        private Education(string institution, string fieldOfStudy, EducationDegree degree, DateRange attendancePeriod, int sellerId, bool isGraduated = false, int id = 0) : base(id)
        {
            Institution = institution;
            FieldOfStudy = fieldOfStudy;
            Degree = degree;
            AttendancePeriod = attendancePeriod;
            SellerId = sellerId;
            IsGraduated = isGraduated;
        }
        /// <summary>
        /// Creates a new Education entity with validation of graduation status and date ranges
        /// </summary>
        /// <param name="institution">Name of the educational institution</param>
        /// <param name="fieldOfStudy">Field or major of study</param>
        /// <param name="degree">Type of degree earned</param>
        /// <param name="start">Start date of education</param>
        /// <param name="end">End date of education (required if graduated)</param>
        /// <param name="sellerId">ID of the seller this education belongs to</param>
        /// <param name="isGraduated">Whether the education was completed</param>
        /// <returns>ErrorOr result containing either the created Education or validation errors</returns>
        public static ErrorOr<Education> Create(string institution, string fieldOfStudy, EducationDegree degree, DateTime start, DateTime? end, int sellerId, bool isGraduated = false)
        {
            if (!isGraduated && degree != EducationDegree.NotGraduated)
            {
                return Error.Conflict("Education.DegreeMismatch", $"Non-graduated education must use {EducationDegree.NotGraduated} degree type");
            }
            var createRangeRes = DateRange.FromDateTimes(start, end, !isGraduated);
            if (createRangeRes.IsError)
            {
                return createRangeRes.FirstError;
            }
            return new Education(institution, fieldOfStudy, degree, createRangeRes.Value, sellerId, isGraduated);

        }
        /// <summary>
        /// Adds a certificate to this education record if none exists
        /// </summary>
        /// <param name="filePath">Path to the certificate file</param>
        /// <param name="description">Description of the certificate</param>
        /// <returns>ErrorOr result indicating success or failure</returns>
        /// <remarks>Each education record can have only one associated certificate</remarks>
        public ErrorOr<Success> AddCertificate(string filePath, string description)
        {
            if (EducationCertificate is not null)
            {
                return Error.Failure("Education.CannotHaveMoreThanOneCertificate", "Already have a certificate related to this education info");
            }
            EducationCertificate = new Certificate(filePath, description, fileId: 0, educationId: Id);
            return Result.Success;
        }
        /// <summary>
        /// Updates the existing education certificate with new file and description
        /// </summary>
        /// <param name="filePath">New path to the certificate file</param>
        /// <param name="description">New description of the certificate</param>
        /// <returns>ErrorOr result indicating success or failure</returns>
        /// <exception cref="Error">Throws if no certificate exists to update</exception>
        public ErrorOr<Success> UpdateCertificate(string filePath, string description, int fileId)
        {
            if (EducationCertificate is null)
            {
                return Error.Conflict("Education.DoNotHaveCertificate", "This education info doesn`t contain a certificate");
            }
            return EducationCertificate.UpdateCertificate(filePath, description, fileId);
        }
        /// <summary>
        /// Removes the certificate associated with this education record
        /// </summary>
        /// <returns>ErrorOr result indicating success or failure</returns>
        /// <exception cref="Error">Throws if no certificate exists to delete</exception>
        public ErrorOr<Success> DeleteCertificate()
        {
            if (EducationCertificate == null)
            {
                return Error.NotFound("Education.NoCertificate", "No certificate exists to delete");
            }

            EducationCertificate = null;
            return Result.Success;
        }
        /// <summary>
        /// Calculates the duration of education in years, months, and days.
        /// </summary>
        /// <returns>A tuple containing the years, months, and days of education duration.</returns>
        public (int Years, int Months, int Days) CalculateEducationDuration()
        {
            return AttendancePeriod.CalculateDuration();
        }
        /// <summary>
        /// Formats the education duration into a human-readable string.
        /// </summary>
        /// <returns>A formatted string representing the education duration.</returns>
        public string FormatEducationDuration()
        {
            return AttendancePeriod.FormatDuration();
        }
        /// <summary>
        /// Updates the education information with the provided details.
        /// </summary>
        /// <param name="institution">The name of the educational institution.</param>
        /// <param name="fieldOfStudy">The field of study.</param>
        /// <param name="degree">The degree obtained or being pursued.</param>
        /// <param name="startDate">The start date of the education period.</param>
        /// <param name="endDate">The end date of the education period.</param>
        /// <param name="isGraduated">Indicates whether the education was completed successfully.</param>
        /// <returns>
        /// An <see cref="ErrorOr{Success}"/> indicating the result of the operation.
        /// Returns Success if the update was successful, or an error if validation fails.
        /// </returns>
        public ErrorOr<Success> UpdateEducation(string institution, string fieldOfStudy, EducationDegree degree, DateTime startDate, DateTime? endDate, bool isGraduated)
        {
            var createRangeRes = DateRange.FromDateTimes(startDate, endDate, isGraduated);
            if (createRangeRes.IsError)
            {
                return createRangeRes.FirstError;
            }
            Institution = institution;
            FieldOfStudy = fieldOfStudy;
            Degree = degree;
            AttendancePeriod = createRangeRes.Value;
            IsGraduated = isGraduated;
            return Result.Success;
        }
        private Education()
        {

        }
    }
}
