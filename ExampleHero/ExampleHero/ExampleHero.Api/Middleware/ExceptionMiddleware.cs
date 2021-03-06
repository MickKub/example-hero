﻿using ExampleHero.Common;
using ExampleHero.Common.Exceptions;
using ExampleHero.Common.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ExampleHero.Api.Middleware
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _next;

		public ExceptionMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await _next(httpContext);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(httpContext, ex);
			}
		}

		private static Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			var responseType = context.Request.Headers[HeaderNames.Accept].FirstOrDefault(x => x == "application/xml") ??
					"application/json";
			context.Response.ContentType = responseType;
			var message = string.Empty;
			if (exception is CustomBaseException)
			{
				context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
				var errorDetails = new ErrorDetails
				{
					Message = ((CustomBaseException) exception).Message,
				};
				message = responseType == "application/xml" ? errorDetails.ToXml() : errorDetails.ToJson();
			}
			else
			{
				context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
				var errorDetails = new ErrorDetails
				{
					Message = "Internal Server Error from the custom middleware",
				};
				message = responseType == "application/xml" ? errorDetails.ToXml() : errorDetails.ToJson();
			}

			return context.Response.WriteAsync(message);
		}
	}
}
