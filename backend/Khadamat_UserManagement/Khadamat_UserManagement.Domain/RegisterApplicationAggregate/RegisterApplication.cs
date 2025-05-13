using ErrorOr;
using Khadamat_UserManagement.Domain.Common;
using Khadamat_UserManagement.Domain.RegisterApplicationAggregate.Events;
using Khadamat_UserManagement.Domain.UserAggregate;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_UserManagement.Domain.RegisterApplicationAggregate
{
    public class RegisterApplication : AggregateRoot
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Email { get; private set; }
        public int Roles { get; set; }
        public ApplicationStatus Status { get; private set; }
        public string? RejectionReason { get; private set; }
        public int? HandledByUserId { get; private set; }
        public RegisterApplication(string username, string password, string email, int roles = 0, int id = 0) : base(id)
        {
            Username = username;
            Password = password;
            Email = email;
            Status = ApplicationStatus.Draft;
            Roles = roles;
            _domainEvents.Add(new ApplicationCreatedDomainEvent(Username, Email));
        }
        public RegisterApplication()
        {
            Debug.WriteLine("");
        }
        public ErrorOr<Success> Approve(User approvedByUser)
        {
            if (!_isDraft())
            {
                return Errors.RegisterApplicationErrors.ApplicationHandled;
            }

            Status = ApplicationStatus.Approved;
            HandledByUserId = approvedByUser.Id;

            _domainEvents.Add(new ApplicationApprovedDomainEvent(Username, Email, approvedByUser.UserName, approvedByUser.Email));

            return Result.Success;
        }
        public ErrorOr<Success> Reject(User rejectedByUser, string rejectionReason)
        {
            if (!_isDraft())
            {
                return Errors.RegisterApplicationErrors.ApplicationHandled;
            }

            Status = ApplicationStatus.Rejected;
            HandledByUserId = rejectedByUser.Id;
            RejectionReason = rejectionReason;
            _domainEvents.Add(new ApplicationRejectedDomainEvent(Username, Email, rejectionReason));

            return Result.Success;

        }
        public void Update(string userame, string email, string password, int roles)
        {
            Username = userame;
            Email = email;
            Password = password;
            Roles = roles;
        }
        private bool _isDraft()
        {
            return Status == ApplicationStatus.Draft;
        }

    }
}
