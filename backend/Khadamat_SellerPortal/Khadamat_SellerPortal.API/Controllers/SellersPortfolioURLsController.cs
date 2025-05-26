using Khadamat_SellerPortal.Application.PortfolioURLs.Commands.AddPortfolioUrl;
using Khadamat_SellerPortal.Application.PortfolioURLs.Commands.DeletePortfolioUrl;
using Khadamat_SellerPortal.Application.PortfolioURLs.Commands.UpdatePortfolioUrl;
using Khadamat_SellerPortal.Contracts.PortfolioURLs;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APIPortfolioType = Khadamat_SellerPortal.Contracts.PortfolioURLs.PortfolioUrlType;
using DomainPortfolioType = Khadamat_SellerPortal.Domain.Common.Entities.PortfolioUrlType;

namespace Khadamat_SellerPortal.API.Controllers
{
    [Route("sellers/{sellerId:int}/portfolio-urls")]
    public class SellersPortfolioURLsController : APIController
    {
        private readonly ISender _mediator;
        public SellersPortfolioURLsController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddPortfolioUrl(int sellerId, [FromBody] AddPortfolioUrlRequest request)
        {
            var command = new AddPortfolioUrlCommand(sellerId, DomainPortfolioType.FromName(request.Type.ToString()), request.Url);
            var result = await _mediator.Send(command);
            return result.Match(onlineSeller =>
            {
                return Ok();
            }, Problem);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdatePortfolioUrl(int sellerId, [FromBody] UpdatePortfolioUrlRequest request)
        {
            var command = new UpdatePortfolioUrlCommand(sellerId, DomainPortfolioType.FromName(request.Type.ToString()), request.Url);
            var updateRes = await _mediator.Send(command);

            return updateRes.Match(onlineSeller =>
            {
                return Ok();
            }, Problem);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeletePortfolioUrl(int sellerId, [FromBody] DeletePortfolioURLRequest request)
        {
            var command = new DeletePortfolioUrlCommand(sellerId, DomainPortfolioType.FromName(request.Type.ToString()));
            var deletingResult = await _mediator.Send(command);
            return deletingResult.Match(_ => NoContent(), Problem);
        }
    }
}
