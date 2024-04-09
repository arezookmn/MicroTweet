using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MicroTweet.Tweets.Api.Models.Contracts;
using MicroTweet.Tweets.Api.Models.Entities;
using MicroTweet.Tweets.Api.Persistence.Context;
using MicroTweet.Tweets.Api.ServiceContracts;
using System.ComponentModel.DataAnnotations;

namespace MicroTweet.Tweets.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TweetsController : ControllerBase
{
    private readonly ITweetServices _tweetServices;
    public TweetsController(ITweetServices services)
    {
        _tweetServices = services;
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CreateTweetRequest tweetRequest)
    {
        try
        {
            await _tweetServices.Post(tweetRequest);
        }
        catch (ValidationException ex) 
        {
            return ValidationProblem(ex.Message);
        }
        return Created();

    }

}
