using Khadamat_SellerPortal.Application.OnlineSellers.Commands;
using Khadamat_SellerPortal.Application.OnlineSellers.Common;
using Khadamat_SellerPortal.Contracts.OnlineSellers;
using Khadamat_SellerPortal.Domain.Common.Entities;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DomainPortfolioType = Khadamat_SellerPortal.Domain.Common.Entities.PortfolioUrlType;
using DomainSocialMediaLinkType = Khadamat_SellerPortal.Domain.Common.Entities.SocialMediaLinkType;
using DomainEducationDegree = Khadamat_SellerPortal.Domain.Common.Entities.EducationDegree;
using APIPortfolioType = Khadamat_SellerPortal.Contracts.OnlineSellers.PortfolioUrlType;
using APISocialMediaLinkType = Khadamat_SellerPortal.Contracts.OnlineSellers.SocialMediaLinkType;
using APIEducationDegree = Khadamat_SellerPortal.Contracts.OnlineSellers.EducationDegree;
using ErrorOr;
using System.Diagnostics;
using Khadamat_SellerPortal.Application.OnlineSellers.Queries.FetchOnlineSeller;
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

        [HttpGet("test")]
        public IActionResult GetTest()
        {
            var filePaths = Directory.GetFiles("D:\\oud\\1");
            var fs = filePaths.Select(Path.GetFileName).ToList();
            return Ok(fs);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> CreateOnlineSeller([FromBody] CreateOnlineSellerRequest request)
        {
            foreach (var education in request.educations)
            {
                var res = ValidateEducationDegreeType(education.degree);
                if (res.IsError)
                {
                    return Problem(res.FirstError);
                }
            }
            foreach (var portfolioUrl in request.portfolioUrls)
            {
                var res = ValidatePortfolioUrlType(portfolioUrl.type);
                if (res.IsError)
                {
                    return Problem(res.FirstError);
                }
            }
            foreach (var socialMediaLink in request.socialMediaLinks)
            {
                var res = ValidateSocialMediaLinkType(socialMediaLink.type);
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
