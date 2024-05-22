namespace MicroTweet.MessagingContracts.Tweet.Events;
public record TweetCreated(string IPAddress, string Text, DateTime CreatedOn);
