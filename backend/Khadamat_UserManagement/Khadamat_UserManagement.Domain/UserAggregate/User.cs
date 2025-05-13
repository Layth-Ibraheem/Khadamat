using ErrorOr;
using Khadamat_UserManagement.Domain.Common;
using Khadamat_UserManagement.Domain.Common.Interfaces;
using Khadamat_UserManagement.Domain.UserAggregate.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_UserManagement.Domain.UserAggregate
{
    public class User : AggregateRoot
    {
        public string UserName { get; private set; }
        private string _hashedPassword;
        public string Email { get; private set; }
        public int Roles { get; private set; }
        public bool IsActive { get; private set; }
        public User(string userName, string hashedPassword, string email, int roles, bool isActive, int id = 0) : base(id)
        {
            UserName = userName;
            _hashedPassword = hashedPassword;
            Roles = roles;
            IsActive = isActive;
            Email = email;
        }

        public ErrorOr<Success> Login(string userName, string password, IPasswordHasher passwordHasher)
        {
            if (!IsActive)
            {
                return Errors.UserErrors.InActiveUser;
            }
            if (userName == UserName && passwordHasher.VerifyPassword(password, _hashedPassword))
            {
                //_domainEvents.Add(new UserLoggedInDomainEvent(Email));
                return Result.Success;
            }
            return Errors.UserErrors.InvalidCredintials;
        }
        public void Register()
        {
            // _domainEvents.Add(new UserRegisteredDomainEvent(UserName, Email));
        }
        public void Update(string email, bool isActive, int roles)
        {
            // There are 2 cases
            // 1- an admin wants to edit a normal user roles so the update operation must happened via an user with an admin role
            // 2- normal user wants to change his password, I think it`s better to seperate this into another method so I can have controle over the roles
            // anothe reason for seperating the reset password is for confirmation purposes so a user will have to confirm the reset operation via email, pin code, etc...
            IsActive = isActive; // if this will have an impact over any thing, I can use a domain event to handle this impact
            Roles = roles;
            Email = email;
        }
        public ErrorOr<Success> ResetPassword(string password, string newPassword, IPasswordHasher passwordHasher)
        {
            if (!passwordHasher.VerifyPassword(password, _hashedPassword))
            {
                return Errors.UserErrors.InvalidCredintials;
            }

            _hashedPassword = passwordHasher.HashPassword(newPassword);

            _domainEvents.Add(new PasswordResetedDomainEvent(Email));

            return Result.Success;
        }
        public void ResetPassword(string newPassword, IPasswordHasher passwordHasher)
        {
            _hashedPassword = passwordHasher.HashPassword(newPassword);
            _domainEvents.Add(new PasswordResetedDomainEvent(Email));
        }

        public void AddRole(UserRole role)
        {
            Roles |= (int)role;
        }

        public void RemoveRole(UserRole role)
        {
            Roles &= ~(int)role;
        }
        public ErrorOr<Success> HasAccessTo(UserRole role)
        {
            if (Roles == (int)UserRole.Admin || ((int)role & Roles) == (int)role)
            {
                return Result.Success;
            }
            else
            {
                return Error.Forbidden(
                    code: "Admin.Errors.AccessDenied",
                    description: $"Access Denied. You don't have the role {role}.");
            }
        }
    }
}
