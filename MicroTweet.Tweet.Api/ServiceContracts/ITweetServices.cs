using MicroTweet.Tweets.Api.Models.Contracts;

namespace MicroTweet.Tweets.Api.ServiceContracts;

public interface ITweetServices
{
    Task Post(CreateTweetRequest request);

}
