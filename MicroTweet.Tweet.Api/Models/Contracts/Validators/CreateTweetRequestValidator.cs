using FluentValidation;

namespace MicroTweet.Tweets.Api.Models.Contracts.Validators;

public class CreateTweetRequestValidator : AbstractValidator<CreateTweetRequest>
{
    public CreateTweetRequestValidator()
    {
        RuleFor(t => t.text)
          .NotEmpty().WithMessage("tweet should have content")
          .NotNull()
          .MaximumLength(500).WithMessage("tweet content should be less that 500 character");

        RuleFor(t => t.UserId)
            .NotEmpty();
    }
}
