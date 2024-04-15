using MicroTweet.Tweets.Api.Helpers;
using MicroTweet.Tweets.Api.Models.BackgroundQueuesContext;
using System.Collections.Concurrent;

namespace MicroTweet.Tweets.Api.BackgrondServices;

public class CreatedTweetBackgroundService(ILogger<CreatedTweetBackgroundService> logger, ConcurrentQueue<CreatedTweetContext> queue) : BackgroundService
{
    private readonly ILogger<CreatedTweetBackgroundService> _logger = logger;
    private readonly ConcurrentQueue<CreatedTweetContext> _queue = queue;

    public override Task StartAsync(CancellationToken cancellationToken )
    {
        _logger.LogInformation("Start create tweet background service ...");
        return base.StartAsync(cancellationToken);
    }
    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Start create tweet background service ...");
        return base.StopAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (stoppingToken.IsCancellationRequested)
        {

            if(_queue.TryDequeue(out var context))
            {
                var hashtags = TextScanner.GetHashtags(context.Text); // send and event to trends service , search service
                var mentions = TextScanner.GetMentions(context.Text); //push event

            }

            await Task.Delay(100);
        }
    }
}
