using Khadamat_SellerPortal.Application.SocialMediaLinks.Commands.AddSocialMediaLink;
using Khadamat_SellerPortal.Application.SocialMediaLinks.Commands.DeleteSocialMediaLink;
using Khadamat_SellerPortal.Application.SocialMediaLinks.Commands.UpdateSocialMediaLink;
using Khadamat_SellerPortal.Contracts.Sellers;
using Khadamat_SellerPortal.Contracts.SocialMediaLinks;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APISocialMediaLinkType = Khadamat_SellerPortal.Contracts.SocialMediaLinks.SocialMediaLinkType;
using DomainSocialMediaLinkType = Khadamat_SellerPortal.Domain.Common.Entities.SocialMediaLinkEntity.SocialMediaLinkType;

namespace Khadamat_SellerPortal.API.Controllers
{
    [Route("sellers/{sellerId:int}/social-media-links")]
    public class SellersSocialMediaLinksController : APIController
    {
        private readonly ISender _mediator;

        public SellersSocialMediaLinksController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddSocialMediaLink(int sellerId, [FromBody] AddSocialMediaLinkRequest request)
        {
            var command = new AddSocialMediaLinkCommand(sellerId, DomainSocialMediaLinkType.FromName(request.Type.ToString()), request.Link);
            var result = await _mediator.Send(command);
            return result.Match(seller =>
            {
                var sellerResponse = seller.Adapt<SellerResponse>();
                return Ok(sellerResponse);
            }, Problem);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateSocialMediaLink(int sellerId, [FromBody] UpdateSocialMediaLinkRequest request)
        {
            var command = new UpdateSocialMediaLinkCommand(sellerId, DomainSocialMediaLinkType.FromName(request.Type.ToString()), request.Link);
            var result = await _mediator.Send(command);
            return result.Match(seller =>
            {
                var sellerResponse = seller.Adapt<SellerResponse>();
                return Ok(sellerResponse);
            }, Problem);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteSocialMediaLink(int sellerId, [FromBody] DeleteSocialMediaLinkRequest request)
        {
            var command = new DeleteSocialMediaLinkCommand(sellerId, DomainSocialMediaLinkType.FromName(request.Type.ToString()));
            var result = await _mediator.Send(command);
            return result.Match(_ => NoContent(), Problem);
        }
    }
}
