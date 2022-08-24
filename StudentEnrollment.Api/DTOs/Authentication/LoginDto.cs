using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace StudentEnrollment.Api.DTOs.Authentication
{
    public class LoginDto
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }

    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.EmailAddress)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(20);
        }
    }
}
