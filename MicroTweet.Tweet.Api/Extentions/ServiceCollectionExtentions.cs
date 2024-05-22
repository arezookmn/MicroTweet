using Mapster;
using Microsoft.AspNetCore.Http;
using MicroTweet.Tweets.Api.BackgrondServices;
using MicroTweet.Tweets.Api.Models.BackgroundQueuesContext;
using MicroTweet.Tweets.Api.Models;
using MicroTweet.Tweets.Api.Persistence.Context;
using MicroTweet.Tweets.Api.ServiceContracts;
using MicroTweet.Tweets.Api.Services;
using System.Collections.Concurrent;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using MassTransit;

namespace MicroTweet.Tweets.Api.Extentions;

public static class ServiceCollectionExtentions
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddHttpContextAccessor();

        services.AddScoped<IUserPrinciple, UserPrinciple>(sp =>
        {
            var httpAccessor = sp.GetRequiredService<IHttpContextAccessor>();
            var httpContext = httpAccessor.HttpContext;
            return new UserPrinciple(httpContext.Connection.RemoteIpAddress.ToString());
        });

        services.AddScoped<ITweetServices, TweetService>();
    }

    public static void RegisterBackgroundServices(this IServiceCollection services)
    {
        services.AddHostedService<CreatedTweetBackgroundService>();
        services.AddSingleton<ConcurrentQueue<CreatedTweetContext>>();

    }

    public static void RegisterMasstransitServices(this IServiceCollection services)
    {
        services.AddMassTransit(configure => configure.UsingInMemory());

    }

    public static void RegisterMapster(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());
        services.AddMapster();
    }

    public static void RegisterDbContext(this IServiceCollection services, IConfiguration configuration)
    {

        services.Configure<TweetDatabaseSettings>(
            configuration.GetSection("TweetDatabase"));

        services.AddDbContext<TweetDbContext>(option =>
        {
            var settings = configuration.GetSection("TweetDatabase").Get<TweetDatabaseSettings>();
            option.UseMongoDB(settings.ConnectionString, settings.DatabaseName);
        });
    }

}





