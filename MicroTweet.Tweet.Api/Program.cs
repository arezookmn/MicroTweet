using FluentValidation;
using Mapster;
using Microsoft.EntityFrameworkCore;
using MicroTweet.Tweets.Api.BackgrondServices;
using MicroTweet.Tweets.Api.Models;
using MicroTweet.Tweets.Api.Models.BackgroundQueuesContext;
using MicroTweet.Tweets.Api.Persistence.Context;
using MicroTweet.Tweets.Api.ServiceContracts;
using MicroTweet.Tweets.Api.Services;
using System.Collections.Concurrent;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IUserPrinciple, UserPrinciple>(sp => {
    var httpAccessor =  sp.GetRequiredService<IHttpContextAccessor>();
    var httpContext = httpAccessor.HttpContext;
    return new UserPrinciple( httpContext.Connection.RemoteIpAddress.ToString());
});


builder.Services.AddHostedService<CreatedTweetBackgroundService>();
builder.Services.AddSingleton<ConcurrentQueue<CreatedTweetContext>>();


builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

var config = TypeAdapterConfig.GlobalSettings;
config.Scan(Assembly.GetExecutingAssembly());
builder.Services.AddMapster();

builder.Services.AddScoped<ITweetServices, TweetService>();

builder.Services.Configure<TweetDatabaseSettings>(
    builder.Configuration.GetSection("TweetDatabase"));

builder.Services.AddDbContext<TweetDbContext>( option =>
{
    var settings = builder.Configuration.GetSection("TweetDatabase").Get<TweetDatabaseSettings>();
    option.UseMongoDB(settings.ConnectionString, settings.DatabaseName);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
 
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
