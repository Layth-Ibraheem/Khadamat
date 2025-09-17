using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Khadamat_SellerPortal.API.Controllers
{
    [Route("sellers/{sellerId:int}/profile-images")]
    public class SellerProfileImagesControllerController : APIController
    {
        private readonly ISender _mediator;

        public SellerProfileImagesControllerController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(int sellerId, IFormFile image)
        {

        }


    }
}
