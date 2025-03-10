﻿namespace Client.Infrastructure.Providers;

public class CustomAuthenticationStateProvider :
    Microsoft.AspNetCore.Components.Authorization.AuthenticationStateProvider
{
    /// <summary>
    /// New
    /// </summary>
    public const string TokenKeyName = "token";

    /// <summary>
    /// New
    /// </summary>
    public CustomAuthenticationStateProvider
        (Blazored.LocalStorage.ILocalStorageService storageService) : base()
    {
        // New
        StorageService = storageService;
        //_storageService = storageService;
    }

    /// <summary>
    /// New
    /// </summary>
    private Blazored.LocalStorage.ILocalStorageService StorageService { get; }
    //private readonly Blazored.LocalStorage.ILocalStorageService _storageService;

    public override async Task
        <Microsoft.AspNetCore.Components.Authorization.AuthenticationState>
        GetAuthenticationStateAsync()
    {
        // ایجاد یک وقفه تصنعی
        await Task
            .Delay(millisecondsDelay: 2000);

        // New
        string? jwtToken =
            await
            StorageService.GetItemAsStringAsync(key: TokenKeyName);

        var claims =
            ParseClaimsFromJwtToken(jwtToken: jwtToken);

        System.Security.Claims.ClaimsIdentity claimsIdentity;

        if (claims is null)
        {
            // Anonymous User
            claimsIdentity =
                new System.Security.Claims.ClaimsIdentity();
        }
        else
        {
            claimsIdentity =
                new System.Security.Claims.ClaimsIdentity
                (claims: claims, authenticationType: "Bearer");
        }

        var claimsPrincipal =
            new System.Security.Claims
            .ClaimsPrincipal(identity: claimsIdentity);

        var authenticationState =
            new Microsoft.AspNetCore
            .Components.Authorization
            .AuthenticationState(user: claimsPrincipal);

        var result =
            await
            Task
            .FromResult(result: authenticationState);

        return result;
    }

    public async Task<bool> LoginAsync(string? jwtToken)
    {
        if (string.IsNullOrWhiteSpace(value: jwtToken) == false)
        {
            await StorageService.SetItemAsStringAsync(key: TokenKeyName, data: jwtToken);

            NotifyAuthenticationStateChanged(task: GetAuthenticationStateAsync());

            return true;
        }

        return false;
    }

    public async Task LogoutAsync()
    {
        await StorageService.RemoveItemAsync(key: TokenKeyName);

        NotifyAuthenticationStateChanged(task: GetAuthenticationStateAsync());
    }


    /// <summary>
    /// Created by Mr. Steve Sanderson
    /// </summary>
    private static IList
        <System.Security.Claims.Claim>? ParseClaimsFromJwtToken(string? jwtToken)
    {
        if (string.IsNullOrWhiteSpace(value: jwtToken))
        {
            return null;
        }

        try
        {
            var payload =
                jwtToken.Split(separator: '.')[1];

            var jsonBytes =
                ParseBase64WithoutPadding(base64: payload);

            var keyValuePairs =
                System.Text.Json.JsonSerializer.Deserialize
                <Dictionary<string, object>>(utf8Json: jsonBytes);

            if (keyValuePairs is null)
            {
                return null;
            }

            // **************************************************
            var result =
                new List<System.Security.Claims.Claim>();

            foreach (var keyValuePair in keyValuePairs)
            {
                var key =
                    keyValuePair.Key;

                var keyValuePairValue =
                    keyValuePair.Value is null ?
                    string.Empty : keyValuePair.Value.ToString();

                string value =
                    string.Empty;

                if (keyValuePairValue is not null)
                {
                    value =
                        keyValuePairValue.ToString();
                }

                var claim =
                    new System.Security.Claims
                    .Claim(type: key, value: value);

                result.Add(item: claim);
            }
            // **************************************************

            // **************************************************
            //var result =
            //	keyValuePairs.Select(current =>
            //		new System.Security.Claims.Claim
            //		(current.Key, current.Key.ToString()));
            // **************************************************

            return result;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Created by Mr. Steve Sanderson
    /// </summary>
    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2:
                {
                    base64 += "==";
                    break;
                }

            case 3:
                {
                    base64 += "=";
                    break;
                }
        }

        var result = Convert
            .FromBase64String(s: base64);

        return result;
    }
}
