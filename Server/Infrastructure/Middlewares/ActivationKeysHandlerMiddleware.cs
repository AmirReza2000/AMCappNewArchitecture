using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Middlewares;

public class ActivationKeysHandlerMiddleware : object
{
	public ActivationKeysHandlerMiddleware
		(Microsoft.AspNetCore.Http.RequestDelegate next) : base()
	{
		Next = next;
	}

	private Microsoft.AspNetCore.Http.RequestDelegate Next { get; }

	/// <summary>
	/// باید اولین پارامتر باشد httpContext
	/// </summary>
	public async System.Threading.Tasks.Task InvokeAsync
		(Microsoft.AspNetCore.Http.HttpContext httpContext,
		Settings.ApplicationSettings applicationSettings,
		Services.Features.Common.HttpContextService httpContextService)
	{
		// **************************************************
		string? domain = null;
		object? validActivationKey = null;

		var errorMessage =
			"No Activation Key! - Call Mr. Amir Reza Barkhordari - (+98)9903333615";
		// **************************************************

		// **************************************************
		domain =
			httpContextService.GetCurrentHostName();

		if (string.IsNullOrWhiteSpace(value: domain))
		{
			await httpContext.Response
				.WriteAsync(text: "1. " + domain + " - " + errorMessage);

			return;
		}
		// **************************************************

		// **************************************************
		var activationKeys =
			applicationSettings.ActivationKeys;

		if (activationKeys is null)
		{
			// using Microsoft.AspNetCore.Http;
			await httpContext.Response
				.WriteAsync(text: "2. " + domain + " - " + errorMessage);

			return;
		}

		if (activationKeys.Length == 0)
		{
			// using Microsoft.AspNetCore.Http;
			await httpContext.Response
				.WriteAsync(text: "3. " + domain + " - " + errorMessage);

			return;
		}
		// **************************************************

		// **************************************************
		validActivationKey =
			GetValidActivationKeyByDomain(domain: domain);

		if (validActivationKey is null)
		{
			// using Microsoft.AspNetCore.Http;
			await httpContext.Response
				.WriteAsync(text: "4. " + domain + " - " + errorMessage);

			return;
		}
		// **************************************************

		// **************************************************
		var validActivationKeyString =
			validActivationKey.ToString();

		if (string.IsNullOrWhiteSpace(value: validActivationKeyString))
		{
			// using Microsoft.AspNetCore.Http;
			await httpContext.Response
				.WriteAsync(text: "5. " + domain + " - " + errorMessage);

			return;
		}
		// **************************************************

		// **************************************************
		var contains =
			activationKeys
			.Where(current => current.ToLower() == validActivationKeyString.ToLower())
			.Any();

		if (contains == false)
		{
			// using Microsoft.AspNetCore.Http;
			await httpContext.Response
				.WriteAsync(text: "6. " + domain + " - " + errorMessage);

			return;
		}
		// **************************************************

		await Next(context: httpContext);
	}

	private static object GetValidActivationKeyByDomain(string domain)
	{
		using var mySHA256 = System.Security.Cryptography.SHA256.Create();

		var stringBuilder =
			new System.Text.StringBuilder();


		domain = domain.Insert(2, "E$p%+E+A=dg$");

		var valueBytes =
			System.Text.Encoding.UTF8.GetBytes(s: domain);

		// Compute the hash of the fileStream.
		byte[] hashBytes =
			mySHA256.ComputeHash(buffer: valueBytes);

		var result =
				System.Convert.ToBase64String(inArray: hashBytes);
		return result;

	}
}