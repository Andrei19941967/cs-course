using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Reminder.Storage.Exceptions;

namespace Reminder.WebApi.Middlewares
{
	public class ExceptionHandlingMiddleware
	{
		private readonly RequestDelegate _next;

		public ExceptionHandlingMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next.Invoke(context);
			}
			catch (ReminderItemNotExistsException)
			{
				SetStatusCode(context, HttpStatusCode.NotFound);
			}
			catch (ReminderItemAlreadyExistsException)
			{
				SetStatusCode(context, HttpStatusCode.Conflict);
			}
		}

		private void SetStatusCode(HttpContext context, HttpStatusCode code)
		{
			context.Response.StatusCode = (int) code;
		}
	}
}