using Mapster;
using MicroTweet.Tweets.Api.Models.Entities;

namespace MicroTweet.Tweets.Api.Models.Contracts.RegisterConfig;

public class TweetRegisterConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<CreateTweetRequest, Tweet>()
            .Map(t => t.Content, tr => tr.text);
    }
}
