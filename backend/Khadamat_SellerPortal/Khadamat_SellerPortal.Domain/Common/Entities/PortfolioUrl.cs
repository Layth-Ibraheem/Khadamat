using ErrorOr;
using Khadamat_SellerPortal.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Domain.Common.Entities
{
    /// <summary>
    /// Represents a portfolio URL associated with an online seller, with type-specific validation rules.
    /// </summary>
    public class PortfolioUrl : Entity
    {
        /// <summary>
        /// Gets the type of portfolio (e.g., Khadamat, Behance, etc.).
        /// </summary>
        public PortfolioUrlType Type { get; private set; }

        /// <summary>
        /// Gets the URL of the portfolio.
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// Gets the ID of the seller who owns this portfolio.
        /// </summary>
        public int SellerId { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PortfolioUrl"/> class.
        /// </summary>
        /// <param name="type">The type of portfolio.</param>
        /// <param name="url">The portfolio URL.</param>
        /// <param name="sellerId">The ID of the owning seller.</param>
        /// <param name="id">The unique identifier (defaults to 0 for new entities).</param>
        private PortfolioUrl(PortfolioUrlType type, string url, int sellerId, int id = 0) : base(id)
        {
            Type = type;
            Url = url;
            SellerId = sellerId;
        }

        /// <summary>
        /// Creates a new portfolio URL with type-specific validation.
        /// </summary>
        /// <remarks>
        /// For Khadamat portfolio types, enforces that the URL must be a khadamat.com domain.
        /// </remarks>
        /// <param name="type">The type of portfolio.</param>
        /// <param name="url">The portfolio URL to validate and create.</param>
        /// <param name="sellerId">The ID of the owning seller.</param>
        /// <param name="id">The unique identifier (defaults to 0 for new entities).</param>
        /// <returns>
        /// A new <see cref="PortfolioUrl"/> instance if validation passes,
        /// or an error if URL validation fails for the specified type.
        /// </returns>
        public static ErrorOr<PortfolioUrl> Create(PortfolioUrlType type, string url, int sellerId, int id = 0)
        {
            if (type == PortfolioUrlType.KhadamatPortfolio)
            {
                if (!url.Contains("khadamat.com"))
                {
                    return Error.Failure("PortfolioUrl.NotKhadamatUrl", "The provided url is not a khadamat portfolio url");
                }
            }
            return new PortfolioUrl(type, url, sellerId, id);
        }

        /// <summary>
        /// Updates the portfolio URL with validation.
        /// </summary>
        /// <param name="newUrl">The new URL to set.</param>
        /// <returns>
        /// <see cref="Success"/> if the URL is valid and was updated,
        /// or an error if the URL is malformed.
        /// </returns>
        public ErrorOr<Success> UpdateUrl(string newUrl)
        {
            if (!Uri.IsWellFormedUriString(newUrl, UriKind.Absolute))
                return Error.Validation("PortfolioUrl.InvalidUrl", "Malformed URL");

            Url = newUrl;
            return Result.Success;
        }
        private PortfolioUrl()
        {
            
        }
    }
}
