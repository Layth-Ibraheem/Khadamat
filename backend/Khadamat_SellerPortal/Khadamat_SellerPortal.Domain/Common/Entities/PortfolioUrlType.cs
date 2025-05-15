using Ardalis.SmartEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Domain.Common.Entities
{
    /// <summary>
    /// Represents the type of portfolio URL associated with an online seller.
    /// Inherits from <see cref="SmartEnum{TEnum}"/> for enhanced enum functionality.
    /// </summary>
    /// <remarks>
    /// This type-safe enum pattern provides predefined portfolio types with validation support.
    /// </remarks>
    public class PortfolioUrlType : SmartEnum<PortfolioUrlType>
    {
        /// <summary>
        /// Git repository portfolio (e.g., GitHub, GitLab, Bitbucket).
        /// </summary>
        public static readonly PortfolioUrlType Git = new PortfolioUrlType("Git", 0);

        /// <summary>
        /// Personal website or custom portfolio.
        /// </summary>
        public static readonly PortfolioUrlType PersonalWebsite = new PortfolioUrlType("PersonalWebsite", 1);

        /// <summary>
        /// Khadamat platform portfolio (requires khadamat.com domain).
        /// </summary>
        public static readonly PortfolioUrlType KhadamatPortfolio = new PortfolioUrlType("KhadamatPortfolio", 2);

        /// <summary>
        /// Initializes a new instance of the <see cref="PortfolioUrlType"/> class.
        /// </summary>
        /// <param name="name">The display name of the portfolio type.</param>
        /// <param name="value">The numeric value of the portfolio type.</param>
        public PortfolioUrlType(string name, int value) : base(name, value)
        {
        }
    }
}
