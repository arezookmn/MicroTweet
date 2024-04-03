namespace MicroTweet.Tweets.Api.Models;

public class TweetDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string TweetsCollectionName { get; set; } = null!;

}
