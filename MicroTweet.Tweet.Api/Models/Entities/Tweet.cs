﻿using MongoDB.Bson;

namespace MicroTweet.Tweets.Api.Models.Entities;

public class Tweet
{
    public ObjectId Id { get; init; }
    public string Content { get; init; }
    public DateTimeOffset CreatedOn { get; init; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedOn { get; set; } = DateTimeOffset.UtcNow;
    public Guid UserId { get; init; }

}
