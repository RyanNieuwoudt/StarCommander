using Microsoft.AspNetCore.Builder;

namespace StarCommander.Middleware.ExceptionHandling;

public static class ExceptionHandlerMiddlewareExtensions
{
	public static IApplicationBuilder UseDeveloperExceptionHandlerMiddleware(this IApplicationBuilder builder) =>
		builder.UseMiddleware<ExceptionHandlerMiddleware>();

	public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder builder) =>
		builder.UseMiddleware<ExceptionHandlerMiddleware>();
}