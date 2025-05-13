using ErrorOr;
using Khadamat_FileService.Domain.FileAggregate;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_FileService.Application.KhadamatFiles.Commands.UploadFile
{
    public record UploadFileCommand(IFormFile File, KhadamatFileReferenceType ReferenceType, string NationalNo, IDictionary<string, string> Metdata) : IRequest<ErrorOr<string>>;
}
