using System.Text.RegularExpressions;

namespace MicroTweet.Tweets.Api.Helpers;

public class TextScanner
{
    public static IEnumerable<string> GetMentions(string text)
    {
        List<string> mentions = new List<string>();

        string pattern = @"@(\w+)";

        MatchCollection matches = Regex.Matches(text, pattern);

        foreach (Match match in matches)
        {
            mentions.Add(match.Groups[1].Value);
        }

        return mentions;
    }
    public static IEnumerable<string> GetHashtags(string text)
    {
        List<string> tags = new List<string>();

        string pattern = @"#(\w+)";

        MatchCollection matches = Regex.Matches(text, pattern);

        foreach (Match match in matches)
        {
            tags.Add(match.Groups[1].Value);
        }

        return tags;
    }
}
