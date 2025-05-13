using Khadamat_FileService.Application.Common.Interfaces;
using Khadamat_FileService.Application.KhadamatFiles.Commands.UploadFile;
using Khadamat_FileService.Application.KhadamatFiles.Queries.DownloadFile;
using Khadamat_FileService.Contracts.Files;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Khadamat_FileService.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FilesController : APIController
    {
        private readonly ISender _mediator;
        public FilesController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("uploadFile")]
        public async Task<IActionResult> UploadFile([FromForm] UploadFileRequest request)
        {
            var metadata = new Dictionary<string, string>();
            if (request.institution != null && request.fieldOfStudy != null)
            {
                metadata.Add("institution", request.institution);
                metadata.Add("fieldOfStudy", request.fieldOfStudy);
            }
            if (request.company != null)
            {
                metadata.Add("company", request.company);
            }

            var command = new UploadFileCommand(
                request.file,
                Domain.FileAggregate.KhadamatFileReferenceType.FromName(request.referenceType.ToString()),
                request.nationalNo,
                metadata);

            var res = await _mediator.Send(command);

            return res.Match(path =>
            {
                return Ok(new { Path = path });
            }, Problem);
        }

        [HttpGet("downloadFile")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFile([FromBody] string path)
        {
            var query = new DownloadFileQuery(path);
            var downloadFileResult = await _mediator.Send(query);
            if (downloadFileResult.IsError)
            {
                return Problem(downloadFileResult.Errors);
            }
            var file = downloadFileResult.Value.File;
            return File(file.FileStream, file.ContentType, file.FileDownloadName);

        }

    }
}
