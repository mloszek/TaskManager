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
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Password).MinimumLength(6);
            RuleFor(x => x.Password).Equal(x => x.ConfirmPassword);
            RuleFor(x => x.Email).Custom((value, validationContext) =>
            {
                var userAlreadyExist = initiativeContext.Users.Any(user => user.Email == value);
                if (userAlreadyExist)
                {
                    validationContext.AddFailure("Email", "This email address is already in use.");
                }
            });
        }
    }
}
