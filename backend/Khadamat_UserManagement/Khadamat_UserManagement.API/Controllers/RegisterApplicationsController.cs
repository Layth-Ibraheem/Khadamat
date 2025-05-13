using Khadamat_UserManagement.Application.RegisterApplications.Commands.ApproveApplication;
using Khadamat_UserManagement.Application.RegisterApplications.Commands.CreateNewRegisterApplication;
using Khadamat_UserManagement.Application.RegisterApplications.Commands.RejectApplication;
using Khadamat_UserManagement.Application.RegisterApplications.Queries.GetAllRegisterApplications;
using Khadamat_UserManagement.Application.RegisterApplications.Queries.GetRegisterApplication;
using Khadamat_UserManagement.Contracts.RegisterApplications;
using Khadamat_UserManagement.Domain.RegisterApplicationAggregate;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DomainAPplicationStatus = Khadamat_UserManagement.Domain.RegisterApplicationAggregate.ApplicationStatus;
using PresintationApplicationStatus = Khadamat_UserManagement.Contracts.RegisterApplications.ApplicationStatus;
namespace Khadamat_UserManagement.API.Controllers
{
    [Route("[controller]")]
    public class RegisterApplicationsController : APIController
    {
        private readonly ISender _mediator;
        public RegisterApplicationsController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("/{Id:int}")]
        public async Task<IActionResult> GetRegisterApplication(int Id)
        {
            var query = new GetRegisterApplicationQuery(Id);
            var result = await _mediator.Send(query);

            return result.Match(application =>
            {
                return Ok(Map(application));
            }, Problem);
        }

        [AllowAnonymous]
        [HttpPost("createNew")]
        public async Task<IActionResult> CreateNewApplication(CreateNewRegisterApplicationRequest request)
        {
            var command = new CreateNewRegisterApplicationCommand(request.Username, request.Email, request.Password);
            var result = await _mediator.Send(command);
            return result.Match(application =>
            {
                return CreatedAtAction(nameof(GetRegisterApplication), new { application.Id }, Map(application));
            }, Problem);
        }

        [HttpPut("{Id:int}/approve")]
        public async Task<IActionResult> Approve(int Id, ApproveApplicationRequest request)
        {
            var command = new ApproveApplicationCommand(Id, request.Roles);
            var result = await _mediator.Send(command);
            return result.Match(_ => Ok(new { Message = "Approved Successfully" }), Problem);
        }

        [HttpPut("{Id:int}/reject")]
        public async Task<IActionResult> Reject(int Id, RejectApplicationRequest request)
        {
            var command = new RejectApplicationCommand(Id, request.RejectionReason);
            var result = await _mediator.Send(command);
            return result.Match(_ => Ok(new { Message = "Rejected Successfully" }), Problem);
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllApplications()
        {
            var query = new GetAllRegisterApplicationsQuery();
            var result = await _mediator.Send(query);
            var applications = new List<RegisterApplicationResponse>();
            foreach (var item in result)
            {
                applications.Add(Map(item));
            }
            return Ok(applications);
        }

        private RegisterApplicationResponse Map(RegisterApplication application)
        {
            return new RegisterApplicationResponse(application.Id, application.Username, application.Email, GetApplicationStatus(application.Status));

        }
        private PresintationApplicationStatus GetApplicationStatus(DomainAPplicationStatus status)
        {
            return status.Name switch
            {
                nameof(DomainAPplicationStatus.Draft) => PresintationApplicationStatus.Draft,
                nameof(DomainAPplicationStatus.Approved) => PresintationApplicationStatus.Approved,
                nameof(DomainAPplicationStatus.Rejected) => PresintationApplicationStatus.Rejected,
                _ => throw new InvalidOperationException(),
            };
        }
    }
}
