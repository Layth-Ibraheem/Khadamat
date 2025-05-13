using FluentValidation;
using Khadamat_UserManagement.Application.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Khadamat_UserManagement.Application.Users.Commands.CreateNewUser
{
    public class CreateNewUserCommandValidator : AbstractValidator<CreateNewUserCommand>
    {
        public CreateNewUserCommandValidator()
        {
            RuleFor(a => a.Email).EmailAddress()
                                 .WithMessage("Not valid email address");

            RuleFor(a => a.Password).NotEmpty()
                                    .MinimumLength(8)
                                    .MaximumLength(50)
                                    .Must(ContainsFunctions.ContainLetters)
                                    .WithMessage("Password must contain at least two letters.")
                                    .Must(ContainsFunctions.ContainDigits)
                                    .WithMessage("Password must contain at least two digits.")
                                    .Must(ContainsFunctions.ContainSpecialChars)
                                    .WithMessage("Password must contain at least one special character.");

            RuleFor(a => a.UserName).NotEmpty()
                                    .MinimumLength(3)
                                    .MaximumLength(50);
        }
    }
}
