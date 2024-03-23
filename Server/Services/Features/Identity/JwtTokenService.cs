using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Features.Identity;

public class JwtTokenService
{
    #region Constructor
    public JwtTokenService(Infrastructure.Settings.
        ApplicationSettings applicationSettings)
    {
        _applicationSettings = applicationSettings;
        _tokenHandler = new JwtSecurityTokenHandler();
        //_tokenParameters = new TokenValidationParameters()
        //{
        //    ValidateIssuer = true,
        //    ValidateAudience = true,
        //    ValidateIssuerSigningKey = true,
        //    ValidateLifetime = true,
        //    ValidIssuer = _applicationSettings.tokenProfile.Issuer!,
        //    ValidAudience = _applicationSettings.tokenProfile.Audience!,
        //    IssuerSigningKey = new SymmetricSecurityKey(
        //     Encoding.ASCII.GetBytes(_applicationSettings.tokenProfile.SecretForKey!)),
        //};
    }

    #endregion /Constructor

    #region Properties
    private Infrastructure.Settings.ApplicationSettings _applicationSettings { get; }
    private JwtSecurityTokenHandler _tokenHandler { get; }
    private TokenValidationParameters _tokenParameters
    {
        get
        {
            return
                new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    //ValidIssuer = _applicationSettings.tokenProfile.Issuer!,
                    //ValidAudience = _applicationSettings.tokenProfile.Audience!,
                    IssuerSigningKey = new SymmetricSecurityKey(
                 Encoding.ASCII.GetBytes
                 (_applicationSettings.tokenProfile.SecretForKey!)),
                };
        }
    }
    #endregion /Properties

    #region public string CreateToken(List<Claim> claims)
    public string? CreateToken(List<Claim> claims)
    {
        var securityKey = new SymmetricSecurityKey(key:
           Encoding.ASCII.GetBytes(_applicationSettings.tokenProfile.SecretForKey!));

        var signingCredentials = new SigningCredentials(
            key: securityKey,
            algorithm: SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _applicationSettings.tokenProfile.Issuer!,
            audience: _applicationSettings.tokenProfile.Audience!,
            //notBefore: DateTime.UtcNow,
            claims: claims,
            expires: _applicationSettings.tokenProfile.ExpiryDate,
            signingCredentials: signingCredentials);

        var tokenToReturn = _tokenHandler.WriteToken(jwtSecurityToken);

        return tokenToReturn;
    }
    #endregion /public string CreateToken(List<Claim> claims)

    #region public bool IsTheTokenValid(string token)
    public async Task<bool> IsTheTokenValidAsync(string token)
    {
        /// <summary>
        /// this also checks the token expiration
        /// </summary>

        var tokenJson = _tokenHandler.ReadToken(token: token) as JwtSecurityToken;
        if (tokenJson == null)
        {

            return false;
        }

        var now = DateTime.UtcNow;

        if (tokenJson.ValidTo < now)
        {

            return false;
        }

        var result = await _tokenHandler.ValidateTokenAsync(token: token
        , validationParameters: _tokenParameters);
        if (result.IsValid)
        {
            return true;
        }
        return false;
    }

    #endregion /public bool IsTheTokenValid(string token)

    #region  public string? GetClaimValue(string type,string value, string token)

    public string? GetClaimValue(string type, string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenJson = tokenHandler.ReadToken(token: token)
            as JwtSecurityToken;
        Claim? claim;
        try
        {        
             claim = tokenJson!.Claims.SingleOrDefault
                (n => n.Type == type);

        }
        catch
        {
            return null;
        }
        if (claim == null)
        {
            return null;
        }
        return claim.Value;

    }
    #endregion /public string? GetClaimValue(string type,string value, string token)

}
