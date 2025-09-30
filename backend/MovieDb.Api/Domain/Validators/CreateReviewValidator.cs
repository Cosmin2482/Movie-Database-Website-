using FluentValidation;
using MovieDb.Api.Domain.DTOs;

namespace MovieDb.Api.Domain.Validators
{
    public class CreateReviewValidator : AbstractValidator<CreateReviewDto>
    {
        public CreateReviewValidator()
        {
            RuleFor(x => x.Text).NotEmpty().MaximumLength(1000);
            RuleFor(x => x.Stars).InclusiveBetween(1,5);
        }
    }
}
