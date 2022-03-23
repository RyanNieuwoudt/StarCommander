using System;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using StarCommander.Infrastructure.Serialization;

namespace StarCommander.Middleware.ExceptionHandling;

public class DeveloperExceptionHandlerMiddleware : ExceptionHandlerMiddleware
{
	public DeveloperExceptionHandlerMiddleware(RequestDelegate next) : base(next)
	{
	}

	protected override string Serialize(Exception ex) =>
		JsonConvert.SerializeObject(new { message = ex.Message, exception = ex }, SerializationSettings.Middleware);
}