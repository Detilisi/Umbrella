using System.Text.RegularExpressions;

namespace Infrastructure.Email.Extensions;

internal static class StringExtensions
{
    public static string ShortText(this string text)
    {
        if (!string.IsNullOrEmpty(text))
        {
            text = text.Trim();
            text = EnsureSingleNewline(text);
            text = ShortenUrlsInText(text);
        }

        return text;
    }

    //Helper function
    private static string EnsureSingleNewline(string input)
    {
        // Use regular expression to replace consecutive newline characters with a single newline
        var result = Regex.Replace(input, @"\n+", "\n");

        return result;
    }
    private static string ShortenUrlsInText(string input)
    {
        // Define a regular expression pattern to match URLs
        string urlPattern = @"(https?://\S+)";

        // Use regex to find all matches
        MatchCollection matches = Regex.Matches(input, urlPattern);

        // Iterate through matches and shorten URLs
        foreach (Match match in matches.Cast<Match>())
        {
            string originalUrl = match.Groups[1].Value;
            string shortenedUrl = originalUrl.Length > 30 ? originalUrl[..30] + "..." : originalUrl;
            input = input.Replace(originalUrl, shortenedUrl);
        }

        return input;
    }
}
