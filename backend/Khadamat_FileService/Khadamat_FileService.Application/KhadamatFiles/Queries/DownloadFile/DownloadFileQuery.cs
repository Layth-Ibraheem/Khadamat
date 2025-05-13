using ErrorOr;
using Khadamat_FileService.Application.KhadamatFiles.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_FileService.Application.KhadamatFiles.Queries.DownloadFile
{
    public record DownloadFileQuery(string Path) : IRequest<ErrorOr<DownloadFileDto>>;
}
