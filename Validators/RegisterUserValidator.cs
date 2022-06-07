using System.Linq;
using FluentValidation;
using TaskManager.Entities;
using TaskManager.Models;

namespace TaskManager.Validators
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserValidator(InitiativeContext initiativeContext)
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Password).MinimumLength(8);
            RuleFor(x => x.Password).MaximumLength(256);
            RuleFor(x => x.Password).Custom((value, validationContext) =>
            {
                var passwordHasAllRequiredCharacters = value.Any(char.IsUpper)
                && value.Any(char.IsLower)
                && value.Any(char.IsNumber)
                && (value.Any(char.IsSymbol) || value.Any(char.IsPunctuation));

                if (!passwordHasAllRequiredCharacters)
                {
                    validationContext.AddFailure("Password", "Password must contain at least one uppercase letter, at least one lowercase letter, at least one number and at least one special character.");
                }
            });
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("Passwords must match.");
            RuleFor(x => x.Email).Custom((value, validationContext) =>
            {
                var userAlreadyExist = initiativeContext.Users.Any(user => user.Email == value);
                if (userAlreadyExist)
                {
                    validationContext.AddFailure("Email", "This email address is already in use.");
                }
            });
            RuleFor(x => x.DateOfBirth).NotEmpty();
        }
    }
}
