using FluentValidation;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using MicroTweet.Tweets.Api.Models.BackgroundQueuesContext;
using MicroTweet.Tweets.Api.Models.Contracts;
using MicroTweet.Tweets.Api.Models.Entities;
using MicroTweet.Tweets.Api.Persistence.Context;
using MicroTweet.Tweets.Api.ServiceContracts;
using System.Collections.Concurrent;

namespace MicroTweet.Tweets.Api.Services;

public class TweetService : ITweetServices
{
    private readonly TweetDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateTweetRequest> _createTweetValidator;
    private readonly ConcurrentQueue<CreatedTweetContext> _queue;
    private readonly IUserPrinciple _userPrinciple;

    public TweetService(TweetDbContext dbContext, IMapper mapper, IValidator<CreateTweetRequest> createTweetValidator, ConcurrentQueue<CreatedTweetContext> queue, IUserPrinciple userPrinciple)
    {
        _createTweetValidator = createTweetValidator;
        _dbContext = dbContext;
        _mapper = mapper;
        _queue = queue;
        _userPrinciple = userPrinciple;
    }
    public async Task Post(CreateTweetRequest request)
    {
        var validationResult = _createTweetValidator.Validate(request);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors); 
        Tweet tweet = _mapper.Map<Tweet>(request);
        _dbContext.Tweets.Add(tweet);
        await _dbContext.SaveChangesAsync();
        _queue.Enqueue(new CreatedTweetContext(tweet.Content, _userPrinciple.IPAddress));
    }
}
