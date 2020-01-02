using System;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace StarCommander.Middleware.ExceptionHandling
{
	public class DeveloperExceptionHandlerMiddleware : ExceptionHandlerMiddleware
	{
		public DeveloperExceptionHandlerMiddleware(RequestDelegate next) : base(next)
		{
		}

		protected override string Serialize(Exception ex)
		{
			return JsonSerializer.Serialize(new { message = ex.Message, Exception = ex });
		}
	}
}