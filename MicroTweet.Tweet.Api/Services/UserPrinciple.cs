using MicroTweet.Tweets.Api.ServiceContracts;

namespace MicroTweet.Tweets.Api.Services;

public class UserPrinciple(string Ip) : IUserPrinciple
{
    //public string Username { get; set; }
    public string IPAddress { get; init; } = Ip;
}
