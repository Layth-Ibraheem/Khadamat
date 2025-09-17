using Khadamat_SellerPortal.Application.OnlineSellers.Common;
using Khadamat_SellerPortal.Application.WorkExperiences.Commands.AddWorkExperience;
using Khadamat_SellerPortal.Application.WorkExperiences.Commands.UpdateWorkExperience;
using Khadamat_SellerPortal.Contracts.Sellers;
using Khadamat_SellerPortal.Contracts.WorkExperiences;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Khadamat_SellerPortal.API.Controllers
{
    [Route("sellers/{sellerId:int}/work-experiences")]
    public class SellersWorkExperiencesController : APIController
    {
        private readonly ISender _mediator;

        public SellersWorkExperiencesController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddWorkExperience(int sellerId, [FromForm] AddWorkExperienceRequest request)
        {
            var certs = new List<CertificateDto>();
            foreach (var cert in request.Certificates)
            {
                certs.Add(new CertificateDto(cert.FilePath, cert.Description, cert.File));
            }
            var d = new AddWorkExperienceCommand(sellerId, request.CompanyName, request.Start, request.End, request.UntilNow, request.Position, request.Field, certs);
            var command = request.Adapt<AddWorkExperienceCommand>();
            command = command with { SellerId = sellerId };
            var result = await _mediator.Send(d);
            return result.Match(seller =>
            {
                var sellerResponse = seller.Adapt<SellerResponse>();
                return Ok(sellerResponse);
            }, Problem);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateWorkExperience(int sellerId, [FromBody] UpdateWorkExperienceRequest request)
        {
            var command = request.Adapt<UpdateWorkExperienceCommand>();
            command = command with { SellerId = sellerId };
            var result = await _mediator.Send(command);
            return result.Match(seller =>
            {
                var sellerResponse = seller.Adapt<SellerResponse>();
                return Ok(sellerResponse);
            }, Problem);
        }
    }
}
