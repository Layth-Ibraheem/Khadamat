using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Domain.Common.ValueObjects
{
    /// <summary>
    /// Represents a seller's geographical address information as a value object.
    /// </summary>
    /// <remarks>
    /// This immutable value object contains country, city, and region information
    /// and provides value-based equality comparison.
    /// </remarks>
    public class SellerAddress : ValueObject
    {
        /// <summary>
        /// Gets the country component of the address.
        /// </summary>
        public string Country { get; private set; }

        /// <summary>
        /// Gets the city component of the address.
        /// </summary>
        public string City { get; private set; }

        /// <summary>
        /// Gets the region/state/province component of the address.
        /// </summary>
        public string Region { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SellerAddress"/> value object.
        /// </summary>
        /// <param name="country">The country name.</param>
        /// <param name="city">The city name.</param>
        /// <param name="region">The region/state/province name.</param>
        public SellerAddress(string country, string city, string region)
        {
            Country = country;
            City = city;
            Region = region;
        }

        /// <summary>
        /// Gets the components used for equality comparison.
        /// </summary>
        /// <returns>
        /// An enumerable containing the country, city and region components
        /// that define this value object's identity.
        /// </returns>
        public override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Country;
            yield return City;
            yield return Region;
        }

        /// <summary>
        /// Returns a string representation of the address in the format:
        /// "Country: {Country}, City: {City}, Region: {Region}".
        /// </summary>
        /// <returns>A formatted string containing all address components.</returns>
        public override string ToString()
        {
            return $"Country: {Country}, City: {City}, Region: {Region}";
        }
        private SellerAddress()
        {
            
        }
    }
}
