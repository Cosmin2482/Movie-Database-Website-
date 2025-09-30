using FluentValidation;
using MovieDb.Api.Domain.DTOs;

namespace MovieDb.Api.Domain.Validators
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.Username).NotEmpty().MinimumLength(3).MaximumLength(32);
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6).MaximumLength(128);
        }
    }
}
