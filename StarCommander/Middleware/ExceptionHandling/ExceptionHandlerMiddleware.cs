using System;
using System.Net;
using System.Security;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using StarCommander.Infrastructure.Serialization;

namespace StarCommander.Middleware.ExceptionHandling;

public class ExceptionHandlerMiddleware
{
	readonly RequestDelegate next;

	public ExceptionHandlerMiddleware(RequestDelegate next)
	{
		this.next = next;
	}

	public async Task Invoke(HttpContext httpContext)
	{
		try
		{
			await next(httpContext);
		}
		catch (SecurityException ex) when (!httpContext.Response.HasStarted)
		{
			await Handle(ex, httpContext, HttpStatusCode.Forbidden);
		}
		catch (Exception ex) when (!httpContext.Response.HasStarted)
		{
			await Handle(ex, httpContext, HttpStatusCode.BadRequest);
		}
	}

	async Task Handle(Exception ex, HttpContext httpContext, HttpStatusCode httpStatusCode)
	{
		httpContext.Response.Clear();
		httpContext.Response.StatusCode = (int)httpStatusCode;
		httpContext.Response.ContentType = @"application/json";

		await httpContext.Response.WriteAsync(Serialize(ex));
	}

	protected virtual string Serialize(Exception ex) =>
		JsonConvert.SerializeObject(new { message = ex.Message }, SerializationSettings.Middleware);
}