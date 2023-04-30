using System.Text.RegularExpressions;

internal partial class PiiScrubber
{
    private const string IpV4AddressRegex = @"[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}";
    private const string IpV6AddressBitsRegex = "[0-9a-f]";
    private const string IpV6WithIpv4AddressRegex = @$"(?:{IpV6AddressBitsRegex}{{0,4}}:|::){{1,6}}{IpV4AddressRegex}";
    private const string IpV6AddressRegex = $"(?:{IpV6AddressBitsRegex}{{0,4}}:|::){{1,7}}{IpV6AddressBitsRegex}{{1,4}}";
    private const string IpV6WithIpv4CombinedRegex = $"{IpV6WithIpv4AddressRegex}|{IpV6AddressRegex}|{IpV4AddressRegex}";
    private const string EmailAddressRegex = "([a-zA-Z0-9'_-]\\.?)*[a-zA-Z0-9'_-]+@[a-zA-Z0-9.-]{1,}\\.[a-zA-Z]{2,}";

    private const int RegexMatchTimeoutInMilliseconds = 1000;

    private static readonly Regex[] s_piiRegexs =
    {
        EmailRegex(),
        IpRegex()
    };

    public string DetectAndTagPII(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            return message;
        }
        foreach (var scrubbingRegex in s_piiRegexs)
        {
            message = Scrube(scrubbingRegex, message);
        }
        return message;
    }

    private string Scrube(Regex piiRegex, string message)
    {
        try
        {
            message = piiRegex.Replace(message, match => HashPiiData(match.Value));
        }
        catch (RegexMatchTimeoutException)
        {
            return HashPiiData(message);
        }
        return message;
    }

    private string HashPiiData(string data)
        => new string('*', data.Length);

    [GeneratedRegex(EmailAddressRegex, RegexOptions.IgnoreCase, RegexMatchTimeoutInMilliseconds)]
    private static partial Regex EmailRegex();

    [GeneratedRegex(IpV6WithIpv4CombinedRegex, RegexOptions.IgnoreCase, RegexMatchTimeoutInMilliseconds)]
    private static partial Regex IpRegex();
}