using Khadamat_SellerPortal.Contracts.OnlineSellers;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using DomainPortfolioType = Khadamat_SellerPortal.Domain.Common.Entities.PortfolioUrlEntity.PortfolioUrlType;
using DomainSocialMediaLinkType = Khadamat_SellerPortal.Domain.Common.Entities.SocialMediaLinkEntity.SocialMediaLinkType;
using DomainEducationDegree = Khadamat_SellerPortal.Domain.Common.Entities.EducationEntity.EducationDegree;
using APIPortfolioType = Khadamat_SellerPortal.Contracts.PortfolioURLs.PortfolioUrlType;
using APISocialMediaLinkType = Khadamat_SellerPortal.Contracts.SocialMediaLinks.SocialMediaLinkType;
using APIEducationDegree = Khadamat_SellerPortal.Contracts.Educations.EducationDegree;
using ErrorOr;
using System.Diagnostics;
using Khadamat_SellerPortal.Application.OnlineSellers.Commands.RegisterOnlineSeller;
using Khadamat_SellerPortal.Application.PortfolioURLs.Commands.UpdatePortfolioUrl;
using Khadamat_SellerPortal.Application.PortfolioURLs.Commands.DeletePortfolioUrl;
using Khadamat_SellerPortal.Application.PortfolioURLs.Commands.AddPortfolioUrl;
using Khadamat_SellerPortal.Application.Sellers.Commands.UpdateOnlineSellerPersonalInfo;
using Khadamat_SellerPortal.Application.Sellers.Queries.FetchSeller;
using Khadamat_SellerPortal.Application.OnlineSellers.Queries.FetchOnlineSeller;
using Khadamat_SellerPortal.Contracts.Sellers;
namespace Khadamat_SellerPortal.API.Controllers
{
    [Route("[controller]")]
    public class OnlineSellersController : APIController
    {
        private readonly ISender _mediator;

        public OnlineSellersController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOnlineSeller(int id)
        {
            var query = new FetchOnlineSellerQuery(id);
            var res = await _mediator.Send(query);
            return res.Match(onlineSeller =>
            {
                var onlineSellerResponse = onlineSeller.Adapt<OnlineSellerResponse>();
                return Ok(onlineSellerResponse);
            }, Problem);
        }

        [HttpPost("register")]
        public async Task<IActionResult> CreateOnlineSeller([FromBody] CreateOnlineSellerRequest request)
        {
            foreach (var education in request.Educations)
            {
                var res = ValidateEducationDegreeType(education.Degree);
                if (res.IsError)
                {
                    return Problem(res.FirstError);
                }
            }
            foreach (var portfolioUrl in request.PortfolioUrls)
            {
                var res = ValidatePortfolioUrlType(portfolioUrl.Type);
                if (res.IsError)
                {
                    return Problem(res.FirstError);
                }
            }
            foreach (var socialMediaLink in request.SocialMediaLinks)
            {
                var res = ValidateSocialMediaLinkType(socialMediaLink.Type);
                if (res.IsError)
                {
                    return Problem(res.FirstError);
                }
            }
            var command = request.Adapt<RegisterNewOnlineSellerCommand>();
            var RegisterOnlineSellerRes = await _mediator.Send(command);
            return RegisterOnlineSellerRes.Match(onlineSeller =>
            {
                var onlineSellerResponse = onlineSeller.Adapt<OnlineSellerResponse>();
                return Ok(onlineSellerResponse);
            }, Problem);

        }


        [HttpPut("{id:int}/updatePersonalInfo")]
        public async Task<IActionResult> UpdatePersonalInfo(int id, [FromBody] UpdateSellerPersonalInfoRequest request)
        {
            var command = request.Adapt<UpdateSellerPersonalInfoCommand>();
            command = command with { SellerId = id };
            var updateRes = await _mediator.Send(command);
            return updateRes.Match(onlineSeller =>
            {
                var onlineSellerResponse = onlineSeller.Adapt<OnlineSellerResponse>();
                return Ok(onlineSellerResponse);
            }, Problem);
        }

        private static ErrorOr<Success> ValidateEducationDegreeType(APIEducationDegree type)
        {
            Debug.WriteLine(type.ToString());
            if (!DomainEducationDegree.TryFromName(type.ToString(), out var _))
            {
                return Error.Validation("NotValidEducationDegreeType", "Education degree type is not valid");
            }
            return Result.Success;
        }
        private static ErrorOr<Success> ValidateSocialMediaLinkType(APISocialMediaLinkType type)
        {
            if (!DomainSocialMediaLinkType.TryFromName(type.ToString(), out var _))
            {
                return Error.Validation("NotValidSocialMediaLinkType", "Social media link type is not valid");
            }

            return Result.Success;
        }
        private static ErrorOr<Success> ValidatePortfolioUrlType(APIPortfolioType type)
        {
            if (!DomainPortfolioType.TryFromName(type.ToString(), out var _))
            {
                return Error.Validation("NotValidPortfolioType", "Portfolio type is not valid");
            }

            return Result.Success;
        }
    }
}
