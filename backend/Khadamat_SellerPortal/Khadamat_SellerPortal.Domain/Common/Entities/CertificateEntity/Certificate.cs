using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Domain.Common.Entities.CertificateEntity
{
    /// <summary>
    /// Represents a certification document associated with either work experience or education.
    /// </summary>
    public class Certificate : Entity
    {
        /// <summary>
        /// Gets the file path where the certificate document is stored, when it is not exists, use file id.
        /// </summary>
        public string CachedFilePath { get; private set; }

        /// <summary>
        /// Gets the description or title of the certificate.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets or sets the ID of the associated work experience, if applicable.
        /// </summary>
        public int? WorkExperienceId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the associated education record, if applicable.
        /// </summary>
        public int? EducationId { get; set; }

        /// <summary>
        /// Gets or sets the FileId of the associated certificate record, if applicable.
        /// </summary>
        public int FileId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Certificate"/> class.
        /// </summary>
        /// <param name="chachedFilePath">Path to the certificate file.</param>
        /// <param name="description">Description or title of the certificate.</param>
        /// <param name="workExperienceId">ID of associated work experience (optional).</param>
        /// <param name="educationId">ID of associated education record (optional).</param>
        /// <param name="id">Unique identifier for the certificate (defaults to 0 for new entities).</param>
        /// <param name="fileId">The ID of the file stored in whatever location</param>
        public Certificate(string chachedFilePath, string description, int fileId, int? workExperienceId = null, int? educationId = null, int id = 0) : base(id)
        {
            CachedFilePath = chachedFilePath;
            Description = description;
            FileId = fileId;
            EducationId = educationId;
            WorkExperienceId = workExperienceId;
        }

        /// <summary>
        /// Updates the certificate's file path and description.
        /// </summary>
        /// <param name="filePath">New path to the certificate file.</param>
        /// <param name="description">New description for the certificate.</param>
        /// <returns>
        /// <see cref="Success"/> if the update was successful,
        /// or an error if the file path doesn't exist.
        /// </returns>
        public ErrorOr<Success> UpdateCertificate(string filePath, string description, int fileId)
        {
            // There is a file service that handle this
            //if (!File.Exists(filePath))
            //{
            //    return Error.Failure("Certificate.FileNotExists", "The provided file path doesn`t exist");
            //}
            CachedFilePath = filePath;
            Description = description;
            FileId = fileId;
            return Result.Success;
        }
        private Certificate()
        {

        }
    }
}
