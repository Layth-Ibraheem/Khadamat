using Khadamat_SellerPortal.Application.SocialMediaLinks.Commands.AddSocialMediaLink;
using Khadamat_SellerPortal.Application.SocialMediaLinks.Commands.DeleteSocialMediaLink;
using Khadamat_SellerPortal.Application.SocialMediaLinks.Commands.UpdateSocialMediaLink;
using Khadamat_SellerPortal.Contracts.SocialMediaLinks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APISocialMediaLinkType = Khadamat_SellerPortal.Contracts.SocialMediaLinks.SocialMediaLinkType;
using DomainSocialMediaLinkType = Khadamat_SellerPortal.Domain.Common.Entities.SocialMediaLinkType;

namespace Khadamat_SellerPortal.API.Controllers
{
    [Route("seller/{sellerId:int}/socialMediaLinks")]
    public class SellersSocialMediaLinksController : APIController
    {
        private readonly ISender _mediator;

        public SellersSocialMediaLinksController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddSocialMediaLink(int sellerId, AddSocialMediaLinkRequest request)
        {
            var command = new AddSocialMediaLinkCommand(sellerId, DomainSocialMediaLinkType.FromName(request.Type.ToString()), request.Link);
            var result = await _mediator.Send(command);
            return result.Match(seller => Ok(seller), Problem);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateSocialMediaLink(int sellerId, UpdateSocialMediaLinkRequest request)
        {
            var command = new UpdateSocialMediaLinkCommand(sellerId, DomainSocialMediaLinkType.FromName(request.Type.ToString()), request.Link);
            var result = await _mediator.Send(command);
            return result.Match(seller => Ok(seller), Problem);
        }

        [HttpPut("delete")]
        public async Task<IActionResult> DeleteSocialMediaLink(int sellerId, DeleteSocialMediaLinkRequest request)
        {
            var command = new DeleteSocialMediaLinkCommand(sellerId, DomainSocialMediaLinkType.FromName(request.Type.ToString()));
            var result = await _mediator.Send(command);
            return result.Match(seller => Ok(seller), Problem);
        }
    }
}
