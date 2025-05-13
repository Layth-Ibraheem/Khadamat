using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_FileService.Application.KhadamatFiles.Common
{
    public record DownloadFileDto(int FileId, FileStreamResult File);
}
