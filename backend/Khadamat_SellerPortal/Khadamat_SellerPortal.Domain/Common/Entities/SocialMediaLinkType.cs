using Ardalis.SmartEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Domain.Common.Entities
{
    /// <summary>
    /// Represents the types of social media platforms supported for seller profiles.
    /// Inherits from <see cref="SmartEnum{TEnum}"/> for enhanced enum functionality.
    /// </summary>
    /// <remarks>
    /// This type-safe enum pattern provides predefined social media platform types
    /// with associated URL validation rules in the <see cref="SocialMediaLink"/> class.
    /// </remarks>
    public class SocialMediaLinkType : SmartEnum<SocialMediaLinkType>
    {
        /// <summary>
        /// Facebook social media platform.
        /// </summary>
        public static readonly SocialMediaLinkType Facebook = new SocialMediaLinkType("Facebook", 0);

        /// <summary>
        /// Instagram social media platform.
        /// </summary>
        public static readonly SocialMediaLinkType Instagram = new SocialMediaLinkType("Instagram", 1);

        /// <summary>
        /// X (formerly Twitter) social media platform.
        /// </summary>
        public static readonly SocialMediaLinkType X = new SocialMediaLinkType("X", 2);

        /// <summary>
        /// YouTube video sharing platform.
        /// </summary>
        public static readonly SocialMediaLinkType Youtube = new SocialMediaLinkType("Youtube", 3);

        /// <summary>
        /// LinkedIn professional networking platform.
        /// </summary>
        public static readonly SocialMediaLinkType Linkedin = new SocialMediaLinkType("Linkedin", 4);

        /// <summary>
        /// Initializes a new instance of the <see cref="SocialMediaLinkType"/> class.
        /// </summary>
        /// <param name="name">The display name of the social media platform.</param>
        /// <param name="value">The numeric value of the platform type.</param>
        public SocialMediaLinkType(string name, int value) : base(name, value)
        {
        }
    }
}
