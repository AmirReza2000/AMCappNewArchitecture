using System;

namespace Infrastructure.Settings;

public class TokenProfile
{
    public TokenProfile()
    {
        SecurityAlgorithms = Microsoft.IdentityModel.Tokens.
            SecurityAlgorithms.HmacSha256;
        SecretForKey =string.Empty;
        Issuer=string.Empty;
        Audience=string.Empty;
        SecurityAlgorithms=string.Empty;
    }
    public string SecretForKey { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string SecurityAlgorithms { get; set; }
    public double ExpirationInMinutes { get; set; }
    public DateTime ExpiryDate
    {
        get
        {
            var result = Framework.DateTime.Now;
            return result.UtcDateTime.
                AddMinutes(ExpirationInMinutes);
        }
    }
}
