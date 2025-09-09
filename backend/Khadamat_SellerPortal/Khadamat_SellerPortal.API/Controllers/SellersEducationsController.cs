using Khadamat_SellerPortal.Application.Educations.Commands.AddEducation;
using Khadamat_SellerPortal.Application.Educations.Commands.UpdateEducation;
using Khadamat_SellerPortal.Contracts.Educations;
using Khadamat_SellerPortal.Contracts.Sellers;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Khadamat_SellerPortal.API.Controllers
{
    [Route("sellers/{sellerId:int}/educations")]
    public class SellersEducationsController : APIController
    {
        private readonly ISender _mediator;
        public SellersEducationsController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddEducation(int sellerId, [FromForm] AddEducationRequest request)
        {
            var command = request.Adapt<AddEducationCommand>();
            command = new AddEducationCommand(sellerId, request.Institution, request.FieldOfStudy,
                Domain.Common.Entities.EducationEntity.EducationDegree.FromName(request.Degree.ToString())
                , request.Start, request.End
                , new Application.OnlineSellers.Common.CertificateDto(request.Certificate.FilePath, request.Certificate.Description, request.Certificate.File), true);
            //command = command with { SellerId = sellerId };
            var result = await _mediator.Send(command);
            return result.Match(seller =>
            {
                var sellerResponse = seller.Adapt<SellerResponse>();
                return Ok(sellerResponse);
            }, Problem);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateEducation(int sellerId, [FromBody] UpdateEducationRequest request)
        {
            var command = request.Adapt<UpdateEducationCommand>();
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
