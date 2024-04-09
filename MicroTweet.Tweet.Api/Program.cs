using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MicroTweet.Tweets.Api.Models;
using MicroTweet.Tweets.Api.Models.Entities;
using MicroTweet.Tweets.Api.Persistence.Context;
using MicroTweet.Tweets.Api.ServiceContracts;
using MicroTweet.Tweets.Api.Services;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

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
