using Microsoft.EntityFrameworkCore;
using MicroTweet.Tweets.Api.Models.Entities;
using System.Reflection;

namespace MicroTweet.Tweets.Api.Persistence.Context;

public class TweetDbContext(DbContextOptions<TweetDbContext> options) : DbContext(options)
{
    public DbSet<Tweet> Tweets { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    }

}
