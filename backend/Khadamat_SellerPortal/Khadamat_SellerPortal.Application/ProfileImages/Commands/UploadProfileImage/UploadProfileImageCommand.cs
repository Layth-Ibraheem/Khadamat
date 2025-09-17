using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_SellerPortal.Application.ProfileImages.Commands.UploadProfileImage
{
    public record UploadProfileImageCommand(int SellerId, IFormFile ImageFile) : IRequest<ErrorOr<Success>>;
}
