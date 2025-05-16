using Khadamat_SellerPortal.Contracts.OnlineSellers;
using Mapster;
using MediatR;
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
using Khadamat_SellerPortal.Application.OnlineSellers.Commands.RegisterOnlineSeller;
using Khadamat_SellerPortal.Application.OnlineSellers.Commands.UpdateOnlineSeller;
using Khadamat_SellerPortal.Application.PortfolioURLs.Commands.UpdatePortfolioUrl;
using Khadamat_SellerPortal.Application.PortfolioURLs.Commands.DeletePortfolioUrl;
using Khadamat_SellerPortal.Application.PortfolioURLs.Commands.AddPortfolioUrl;
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
            var command = request.Adapt<UpdateOnlineSellerPersonalInfoCommand>();
            command = command with { SellerId = id };
            var updateRes = await _mediator.Send(command);
            return updateRes.Match(onlineSeller =>
            {
                var onlineSellerResponse = onlineSeller.Adapt<OnlineSellerResponse>();
                return Ok(onlineSellerResponse);
            }, Problem);
        }

        [HttpPut("{id:int}/updatePortfolioUrl")]
        public async Task<IActionResult> UpdatePortfolioUrl(int id, [FromBody] UpdatePortfolioUrlRequest request)
        {
            var command = new UpdatePortfolioUrlCommand(id, DomainPortfolioType.FromName(request.Type.ToString()), request.Url);
            var updateRes = await _mediator.Send(command);

            return updateRes.Match(onlineSeller =>
            {
                var onlineSellerResponse = onlineSeller.Adapt<OnlineSellerResponse>();
                return Ok(onlineSellerResponse);
            }, Problem);
        }

        [HttpDelete("{id:int}/deletePortfolio")]
        public async Task<IActionResult> DeletePortfolioUrl(int id, [FromBody] DeletePortfolioURLRequest request)
        {
            var command = new DeletePortfolioUrlCommand(id, DomainPortfolioType.FromName(request.Type.ToString()));
            var deletingResult = await _mediator.Send(command);
            return deletingResult.Match(onlineSeller =>
            {
                var onlineSellerResponse = onlineSeller.Adapt<OnlineSellerResponse>();
                return Ok(onlineSellerResponse);
            }, Problem);
        }

        [HttpPost("{id:int}/addPortfolioUrl")]
        public async Task<IActionResult> AddPortfolioUrl(int id, [FromBody] AddPortfolioUrlRequest request)
        {
            var command = new AddPortfolioUrlCommand(id, DomainPortfolioType.FromName(request.Type.ToString()), request.Url);
            var result = await _mediator.Send(command);
            return result.Match(onlineSeller =>
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
