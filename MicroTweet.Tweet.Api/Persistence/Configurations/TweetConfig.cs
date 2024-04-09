using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MicroTweet.Tweets.Api.Models.Entities;
using MongoDB.EntityFrameworkCore.Extensions;

namespace MicroTweet.Tweets.Api.Persistence.Configurations;

public class TweetConfig : IEntityTypeConfiguration<Tweet>
{
    public void Configure(EntityTypeBuilder<Tweet> builder)
    {
        builder.ToCollection("Tweets");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Content)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(t => t.UserId)
            .IsRequired();
    }
}
