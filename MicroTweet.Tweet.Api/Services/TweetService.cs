using FluentValidation;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using MicroTweet.Tweets.Api.Models.Contracts;
using MicroTweet.Tweets.Api.Models.Entities;
using MicroTweet.Tweets.Api.Persistence.Context;
using MicroTweet.Tweets.Api.ServiceContracts;

namespace MicroTweet.Tweets.Api.Services;

public class TweetService : ITweetServices
{
    private readonly TweetDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateTweetRequest> _createTweetValidator;
    public TweetService(TweetDbContext dbContext, IMapper mapper, IValidator<CreateTweetRequest> createTweetValidator)
    {
        _createTweetValidator = createTweetValidator;
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task Post(CreateTweetRequest request)
    {
        var validationResult = _createTweetValidator.Validate(request);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors); 
        Tweet tweet = _mapper.Map<Tweet>(request);
        _dbContext.Tweets.Add(tweet);
        await _dbContext.SaveChangesAsync();
    }
}
