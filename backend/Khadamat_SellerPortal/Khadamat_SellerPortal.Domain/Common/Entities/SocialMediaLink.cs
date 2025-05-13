    using ErrorOr;
using System.Text.RegularExpressions;

namespace Khadamat_SellerPortal.Domain.Common.Entities
{
    /// <summary>
    /// Represents a social media link associated with an online seller, with platform-specific validation.
    /// </summary>
    public class SocialMediaLink : Entity
    {
        /// <summary>
        /// Gets the URL of the social media profile.
        /// </summary>
        public string Link { get; private set; }

        /// <summary>
        /// Gets the type of social media platform.
        /// </summary>
        public SocialMediaLinkType Type { get; private set; }

        /// <summary>
        /// Gets the ID of the seller who owns this social media link.
        /// </summary>
        public int SellerId { get; private set; }

        /// <summary>
        /// Dictionary containing validation rules for each social media type.
        /// Uses regular expressions to validate URL formats per platform.
        /// </summary>
        private static readonly Dictionary<SocialMediaLinkType, Regex> _validationRules = new()
        {
            [SocialMediaLinkType.Facebook] = new Regex(
                @"^(https?:\/\/)?(www\.)?(facebook|fb)\.com\/[a-zA-Z0-9\.\-_]+/?$",
                RegexOptions.IgnoreCase | RegexOptions.Compiled),

            [SocialMediaLinkType.Instagram] = new Regex(
                @"^(https?:\/\/)?(www\.)?instagram\.com\/[a-zA-Z0-9\.\-_]+/?$",
                RegexOptions.IgnoreCase | RegexOptions.Compiled),

            [SocialMediaLinkType.X] = new Regex(
                @"^(https?:\/\/)?(www\.)?(x|twitter)\.com\/[a-zA-Z0-9_]+/?$",
                RegexOptions.IgnoreCase | RegexOptions.Compiled),

            [SocialMediaLinkType.Youtube] = new Regex(
                @"^(https?:\/\/)?(www\.)?(youtube\.com|youtu\.be)\/(channel\/|user\/)?[a-zA-Z0-9\-_]+/?$",
                RegexOptions.IgnoreCase | RegexOptions.Compiled),

            [SocialMediaLinkType.Linkedin] = new Regex(
                @"^(https?:\/\/)?(www\.)?linkedin\.com\/(in|company)\/[a-zA-Z0-9\-_]+\/?$",
                RegexOptions.IgnoreCase | RegexOptions.Compiled),
            //[SocialMediaLinkType.Tiktok] = new Regex(
            //@"^(https?:\/\/)?(www\.)?tiktok\.com\/@[a-zA-Z0-9\.\-_]+\/?$",
            //RegexOptions.IgnoreCase | RegexOptions.Compiled),

            //[SocialMediaLinkType.Pinterest] = new Regex(
            //@"^(https?:\/\/)?(www\.)?pinterest\.(com|fr)\/[a-zA-Z0-9\-_]+\/?$",
            //RegexOptions.IgnoreCase | RegexOptions.Compiled)
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="SocialMediaLink"/> class.
        /// </summary>
        /// <param name="type">The type of social media platform.</param>
        /// <param name="link">The validated URL of the social media profile.</param>
        /// <param name="sellerId">The ID of the owning seller.</param>
        /// <param name="id">The unique identifier (defaults to 0 for new entities).</param>
        private SocialMediaLink(SocialMediaLinkType type, string link, int sellerId, int id = 0) : base(id)
        {
            Type = type;
            Link = link;
            SellerId = sellerId;
        }

        /// <summary>
        /// Creates a new social media link with platform-specific validation.
        /// </summary>
        /// <param name="link">The URL to validate and create.</param>
        /// <param name="type">The type of social media platform.</param>
        /// <param name="sellerId">The ID of the owning seller.</param>
        /// <param name="id">The unique identifier (defaults to 0 for new entities).</param>
        /// <returns>
        /// A new <see cref="SocialMediaLink"/> instance if validation passes,
        /// or an error if URL validation fails for the specified platform type.
        /// </returns>
        public static ErrorOr<SocialMediaLink> Create(string link, SocialMediaLinkType type, int sellerId, int id = 0)
        {
            var res = _ValidateLinkPerType(type, link);
            if (res.IsError)
            {
                return res.FirstError;
            }
            return new SocialMediaLink(type, link, sellerId, id);
        }

        /// <summary>
        /// Validates a social media link against platform-specific rules.
        /// </summary>
        /// <param name="type">The social media platform type to validate against.</param>
        /// <param name="link">The URL to validate.</param>
        /// <returns>
        /// <see cref="Success"/> if validation passes,
        /// or an error if the URL is empty, malformed, or doesn't match the platform pattern.
        /// </returns>
        private static ErrorOr<Success> _ValidateLinkPerType(SocialMediaLinkType type, string link)
        {
            if (string.IsNullOrWhiteSpace(link))
                return Error.Validation("SocialMediaLink.Empty", "URL cannot be empty");

            if (!Uri.TryCreate(link, UriKind.Absolute, out _))
                return Error.Validation("SocialMediaLink.Invalid", "Malformed URL");

            if (!_validationRules.TryGetValue(type, out var regex))
                return Error.NotFound("SocialMediaLink.UnknownType", "Unsupported social media type");

            return regex.IsMatch(link)
                ? Result.Success
                : Error.Validation(
                    code: "SocialMediaLink.InvalidFormat",
                    description: $"Invalid {type} URL format. Expected pattern: {regex}");
        }

        /// <summary>
        /// Updates the social media link with validation.
        /// </summary>
        /// <param name="link">The new URL to set.</param>
        /// <returns>
        /// <see cref="Success"/> if the URL is valid and was updated,
        /// or an error if validation fails for the current platform type.
        /// </returns>
        public ErrorOr<Success> UpdateLink(string link)
        {
            var res = _ValidateLinkPerType(Type, link);
            if (res.IsError)
            {
                return res.FirstError;
            }
            Link = link;
            return Result.Success;
        }
        private SocialMediaLink()
        {
            
        }
    }
}
