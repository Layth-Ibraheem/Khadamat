using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Domain.Common.ValueObjects
{
    /// <summary>
    /// Represents the personal details of a seller as a value object.
    /// </summary>
    /// <remarks>
    /// This immutable value object contains personal identification information
    /// and address details, with value-based equality comparison.
    /// </remarks>
    public class SellerPersonalDetails : ValueObject
    {
        /// <summary>
        /// Gets the seller's first name.
        /// </summary>
        public string FirstName { get; private set; }

        /// <summary>
        /// Gets the seller's second/middle name.
        /// </summary>
        public string SecondName { get; private set; }

        /// <summary>
        /// Gets the seller's last name.
        /// </summary>
        public string LastName { get; private set; }

        /// <summary>
        /// Gets the seller's email address.
        /// </summary>
        public string Email { get; private set; }

        /// <summary>
        /// Gets the seller's national identification number.
        /// </summary>
        public string NationalNo { get; private set; }

        /// <summary>
        /// Gets the seller's date of birth.
        /// </summary>
        public DateTime DateOfBirth { get; private set; }

        /// <summary>
        /// Gets the seller's address information.
        /// </summary>
        public SellerAddress Address { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SellerPersonalDetails"/> value object.
        /// </summary>
        /// <param name="firstName">The seller's first name.</param>
        /// <param name="secondName">The seller's second/middle name.</param>
        /// <param name="lastName">The seller's last name.</param>
        /// <param name="email">The seller's email address.</param>
        /// <param name="nationalNo">The seller's national identification number.</param>
        /// <param name="dateOfBirth">The seller's date of birth.</param>
        /// <param name="country">The country component of the address.</param>
        /// <param name="city">The city component of the address.</param>
        /// <param name="region">The region/state/province component of the address.</param>
        public SellerPersonalDetails(
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
            FirstName = firstName;
            SecondName = secondName;
            LastName = lastName;
            Email = email;
            NationalNo = nationalNo;
            DateOfBirth = dateOfBirth;
            Address = new SellerAddress(country, city, region);
        }

        /// <summary>
        /// Gets the components used for equality comparison.
        /// </summary>
        /// <returns>
        /// An enumerable containing all personal details components
        /// that define this value object's identity, including nested address components.
        /// </returns>
        public override IEnumerable<object?> GetEqualityComponents()
        {
            yield return FirstName;
            yield return SecondName;
            yield return LastName;
            yield return Email;
            yield return NationalNo;
            yield return DateOfBirth;
            yield return Address.GetEqualityComponents();
        }
        private SellerPersonalDetails()
        {
            
        }
    }
}
