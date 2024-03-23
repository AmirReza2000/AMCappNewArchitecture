using Azure.Core;
using Constants;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Infrastructure.Middlewares;

public class GlobalExceptionHandlerMiddleware : object
{
	private ILogger<GlobalExceptionHandlerMiddleware> logger {  get;}
	public GlobalExceptionHandlerMiddleware(RequestDelegate next,
		ILogger<GlobalExceptionHandlerMiddleware> _logger) : base()
	{
		Next = next;
		logger = _logger;
	}

	#region Properties

	private Microsoft.AspNetCore.Http.RequestDelegate Next { get; init; }

	#endregion /Properties

	#region Methods

	#region InvokeAsync()
	public async System.Threading.Tasks.Task InvokeAsync
		(Microsoft.AspNetCore.Http.HttpContext httpContext)
	{
		try
		{
			await Next(context: httpContext);

			switch (httpContext.Response.StatusCode)
			{
				case (int)System.Net.HttpStatusCode.NotFound:
				{
					if (httpContext.Response.HasStarted == false)
					{
						httpContext.Request.Path = "/Errors/Error404";

						// TODO
						await Next(context: httpContext);
					}

					break;
				}
			}
		}
		catch (System.Exception ex)
		{
			logger.LogError(ex.InnerException,ex.Message);
			httpContext.Response.StatusCode=StatusCodes.Status500InternalServerError;
			httpContext.Response.Headers.ContentType = ContentTypes.InternalServerError;
			await httpContext.Response.WriteAsJsonAsync(
				new
				{
                    status = StatusCodes.Status500InternalServerError,
					instance = httpContext.Request.Path.Value,
                });
		}
	}
	#endregion /InvokeAsync()

	#endregion /Methods
}
