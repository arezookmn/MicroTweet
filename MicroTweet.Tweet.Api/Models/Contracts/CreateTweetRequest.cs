namespace MicroTweet.Tweets.Api.Models.Contracts;

public record CreateTweetRequest(string text, Guid UserId);
