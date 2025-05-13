using ErrorOr;
using Khadamat_UserManagement.Domain.RegisterApplicationAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_UserManagement.Application.RegisterApplications.Commands.CreateNewRegisterApplication
{
    public record CreateNewRegisterApplicationCommand(string Username, string Email, string Password) : IRequest<ErrorOr<RegisterApplication>>;
}
